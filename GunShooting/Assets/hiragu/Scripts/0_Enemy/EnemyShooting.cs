using System.ComponentModel;
using UnityEngine;

public class EnemyShooting : MonoBehaviour, IEnemyDeathListener
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingInterval = 5.0f;
    [SerializeField] float bulletSpeed = 10.0f;

    Transform player;
    Transform targetPoint;
    float countTime = 0;
    GameObject bullet;

    Rigidbody bulletRb;

    Vector3 enemyDirectionControl;
    Vector3 bulletDirection;

    [SerializeField] bool isShootingEne = false;
    [SerializeField] bool isBoss = false;
    bool isPinch = false;
    EnemyHealth enmeyHealth;

    [Header("ボスのHPが減った時に強化する")]
    [SerializeField] float pinchShootingInterval = 0.7f;
    [SerializeField] float pinchBulletSpeed = 13f;
    [SerializeField] int pinchHp = 100;

    Animator anim;
    bool isDie = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetPoint = player.Find("AimPoint");
        if(isBoss)
        {
            enmeyHealth = GetComponent<EnemyHealth>();
        }

        if (isShootingEne)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) return;
        if (player == null) return;

        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        
        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            Shooting();

            countTime = 0;
        }

        if(isBoss && enmeyHealth.EnmeyCurrentHp <= pinchHp && !isPinch)
        {
            shootingInterval = pinchShootingInterval;
            bulletSpeed = pinchBulletSpeed;
            isPinch = true;
            Debug.Log("強くなった");
        }
    }

    void Shooting()
    {
        if (player == null) return;
        if (isDie) return;
        // 射撃アニメーション
        if (isShootingEne && anim != null)
        {
            anim.SetBool("Shooting", true);
        }

        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(
            (targetPoint.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletDirection = (targetPoint.position - shootingPoint.position).normalized;
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;
        AudioManager.Instance.PlaySE3D(SEType.Gun, transform.position);
        Destroy(bullet, 5);

        Invoke(nameof(EndShooting), 0.2f);
    }

    public void OnDeath()
    {
        isDie = true;

        anim.SetBool("Die", true);
        Debug.Log("アニメーション再生");
        Destroy(gameObject, 0.8f);
    }

    void EndShooting()
    {
        if (isDie) return;
        if (isShootingEne && anim != null)
        {
            anim.SetBool("Shooting", false);
        }
    }

}
