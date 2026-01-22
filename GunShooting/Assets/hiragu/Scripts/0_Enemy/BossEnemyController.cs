using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyController : MonoBehaviour, IEnemyDeathListener
{
    [Header("右手の銃の設定(ロケランとホーミング)")]
    [SerializeField] float stopDistance = 10f;              // 一定距離まで近づく
    [SerializeField] Transform shootingPoint;               // 発射位置
    //[SerializeField] GameObject bulletPrefab;               // 発射する弾
    [SerializeField] float shootingInterval = 3.0f;         // 発射間隔
    [SerializeField] float homingBulletSpeed = 5;
    [SerializeField] float locLanSpeed = 5;
    [SerializeField] List<GameObject> bulletList;

    [Header("左手の銃の設定(通常の弾)")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform leftShootingPoint;
    [SerializeField] float leftShootingInterval = 5.0f;
    [SerializeField] float bulletSpeed = 10.0f;

    [Header("ボスのHPが減った時に強化する")]
    [SerializeField] float pinchShootingInterval = 0.7f;
    [SerializeField] float pinchBulletSpeed = 13f;
    [SerializeField] int pinchHp = 100;

    Transform player;                                       // プレイヤーを追いかけるための位置
    NavMeshAgent agent;                                     // 追いかけるためのもの
    float distance;                                         // 一定距離で追いかけることをやめるためのもの
    Transform targetPoint;
    EnemyHealth enemyHealth;
    GameObject bullet;                                      // これに弾をセット
    Rigidbody bulletRb;
    Vector3 bulletDirection;
    Vector3 enemyDirectionControl;                          // 常にプレイヤーのほうを向くため
    float countTime = 0;                                    // 発射間隔制御用

    bool isPinch = false;
    bool isDie;
    bool isFootstepPlaying;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetPoint = player.Find("AimPoint");
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }

        distance = Vector3.Distance(transform.position, player.position);
        if (isDie) return;

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }

        if(agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isWalk", true);
        }else if (agent.velocity.magnitude <= 0.1f)
        {
            anim.SetBool("isWalk", false);
        }


            

        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            //bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            int rand = Random.Range(0, bulletList.Count);
            bullet = Instantiate(bulletList[rand],shootingPoint.position, shootingPoint.rotation);
            LocketLauncherBulletController llbc = bullet.GetComponent<LocketLauncherBulletController>();
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            if(llbc != null)
            {
                llbc.setShootingPoint(shootingPoint);
                llbc.bulletSpeed = locLanSpeed;
            }
            if (hb != null)
            {
                hb.bulletSpeed = homingBulletSpeed;
            }
            AudioManager.Instance.PlaySE3D(SEType.Gun, transform.position);
            Destroy(bullet, 5);
            anim.SetBool("isShootRight", true);
            Invoke(nameof(StopShootRight), 0.01f);

            countTime = 0;
        }


        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            Shooting();

            countTime = 0;
        }

        if (enemyHealth.EnmeyCurrentHp <= pinchHp && !isPinch)
        {
            shootingInterval = pinchShootingInterval;
            bulletSpeed = pinchBulletSpeed;
            isPinch = true;
            Debug.Log("強くなった");
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            if (!isFootstepPlaying)
            {
                AudioManager.Instance.PlaySE3D(SEType.EnemyWalk, transform.position);
                isFootstepPlaying = true;
                Invoke(nameof(ResetFootstep), 10.0f); // 音の長さ
            }
        }
        else
        {
            isFootstepPlaying = false;
        }
    }

    void ResetFootstep()
    {
        isFootstepPlaying = false;
    }

    void Shooting()
    {
        if (player == null) return;
        if (isDie) return;
        

        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(
            (targetPoint.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletDirection = (targetPoint.position - shootingPoint.position).normalized;
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;
        AudioManager.Instance.PlaySE3D(SEType.Gun, transform.position);

        anim.SetBool("isShootLeft", true);
        Invoke(nameof(StopShootLeft), 0.01f);

        Destroy(bullet, 5);

       
    }

    void StopShootRight()
    {
        anim.SetBool("isShootRight", false);
    }

    void StopShootLeft()
    {
        anim.SetBool("isShootLeft", false);
    }

    public void OnDeath()
    {
        isDie = true;

        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        anim.SetBool("isDie", true);
        Debug.Log("アニメーション再生");
        agent.enabled = false;
        Destroy(gameObject, 0.8f);
    }
}
