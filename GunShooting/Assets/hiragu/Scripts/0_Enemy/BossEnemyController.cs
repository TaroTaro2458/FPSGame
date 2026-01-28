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
    [SerializeField] Transform shootingPoint;               // 発射位置(右手)
    [SerializeField] float shootingInterval = 3.0f;         // 発射間隔
    [SerializeField] float homingBulletSpeed = 5;           // ホーミングのスピード
    [SerializeField] float locLanSpeed = 5;                 // ロケランのスピード
    [SerializeField] List<GameObject> bulletList;           // 弾のリスト

    [Header("左手の銃の設定(通常の弾)")]
    [SerializeField] GameObject bulletPrefab;               // 弾のプレハブ
    [SerializeField] Transform leftShootingPoint;           // 発射位置(左手)
    [SerializeField] float leftShootingInterval = 5.0f;     // 発射間隔
    [SerializeField] float bulletSpeed = 10.0f;             // 弾のスピード

    [Header("ボスのHPが減った時に強化する")]
    [SerializeField] float pinchShootingInterval = 0.7f;    // ピンチになった時の右手の射撃の発射間隔
    [SerializeField] float pinchBulletSpeed = 13f;          // 弾のスピード
    [SerializeField] int pinchHp = 100;                     // このHP以下になったら強化する

    Transform player;                                       // プレイヤーを追いかけるための位置
    NavMeshAgent agent;                                     // 追いかけるためのもの
    float distance;                                         // 一定距離で追いかけることをやめるためのもの
    Transform targetPoint;                                  // これに向けて弾を撃つ
    EnemyHealth enemyHealth;                                // ピンチかどうかを判断するために必要
    GameObject bullet;                                      // これに弾をセット
    Rigidbody bulletRb;                                     // 弾を動かすためのリジットボディ
    Vector3 bulletDirection;                                // 最終的に弾を飛ばす位置
    Vector3 enemyDirectionControl;                          // 常にプレイヤーのほうを向くため
    float countTime = 0;                                    // 発射間隔制御用 右手
    float countTimeLeft = 0;                                // 発射間隔制御用 左手
    Animator anim;                                          // アニメーション

    bool isPinch = false;                                   // ピンチの時を判断
    bool isDie;                                             // 死んでいるか判断
    bool isFootstepPlaying;                                 // 足音がなっているか判断
    bool isWalkingNow;                                      // アニメーションを制御するために今のフレームの状態を保持するためのもの
                                

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
        if (isDie) return;
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }

        distance = Vector3.Distance(transform.position, player.position);
        
        // プレイヤーとの距離が一定以内になるまで追いかける
        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }

        isWalkingNow = agent.velocity.magnitude > 0.1f;

        // 歩くアニメーションと停止時のアニメーションの切り替え
        anim.SetBool("isWalk",isWalkingNow);

        // Y軸以外はプレイヤーのほうを向く
        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        // 右手の射撃の処理
        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            ShootingRight();
        }

        // 左手の射撃の処理
        countTimeLeft += Time.deltaTime;
        if (countTimeLeft > leftShootingInterval)
        {
            Shooting();
        }

        // HPが一定以下になったら強くする
        if (enemyHealth.EnmeyCurrentHp <= pinchHp && !isPinch)
        {
            shootingInterval = pinchShootingInterval;
            bulletSpeed = pinchBulletSpeed;
            isPinch = true;
            Debug.Log("強くなった");
        }

        // 足音のSE
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

    // 足音を鳴らすときの制御に使う
    void ResetFootstep()
    {
        isFootstepPlaying = false;
    }

    // 右手(ロケラン、ホーミングのやつ)の射撃処理
    void ShootingRight()
    {
        // プレイヤーがいなかったり、自分が死んでいたりしたときに動かさないため
        if (player == null) return;
        if (isDie) return;

        // 弾のリストからランダムに選ぶ
        int rand = Random.Range(0, bulletList.Count);
        bullet = Instantiate(bulletList[rand], shootingPoint.position, shootingPoint.rotation);

        // 選ばれた弾のコンポーネントを取得
        LocketLauncherBulletController llbc = bullet.GetComponent<LocketLauncherBulletController>();
        HomingBullet hb = bullet.GetComponent<HomingBullet>();

        // ロケランを発射する
        if (llbc != null)
        {
            llbc.setShootingPoint(shootingPoint);
            llbc.bulletSpeed = locLanSpeed;
        }
        // ホーミング弾を発射する
        if (hb != null)
        {
            hb.bulletSpeed = homingBulletSpeed;
        }

        // 発射時のSE
        AudioManager.Instance.PlaySE3D(SEType.Gun, transform.position);
        Destroy(bullet, 5);

        // 停止時か歩いているときのアニメーションを再生
        if (agent.velocity.magnitude > 0.1)
        {
            anim.SetTrigger("ShootWalkRight");
        }
        else
        {
            anim.SetTrigger("ShootIdelRight");
        }
        // 次に撃つまでの時間をリセット
        countTime = 0;
    }

    // 左手(通常)の射撃処理
    void Shooting()
    {
        // プレイヤーがいなかったり、自分が死んでいたりしたときに動かさないため
        if (player == null) return;
        if (isDie) return;
        
        // 弾を生成してプレイヤーの胸あたりに飛ばす処理
        bullet = Instantiate(bulletPrefab, leftShootingPoint.position, leftShootingPoint.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(
            (targetPoint.position - leftShootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletDirection = (targetPoint.position - leftShootingPoint.position).normalized;
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;

        // 発射時のSE
        AudioManager.Instance.PlaySE3D(SEType.Gun, transform.position);

        // 停止時か歩いているときのアニメーションを再生
        if (agent.velocity.magnitude > 0.1)
        {
            anim.SetTrigger("ShootWalkLeft");
        }else
        {
            anim.SetTrigger("ShootIdelLeft");
        }
        // 次に撃つまでの時間をリセット
        countTimeLeft = 0;
        // 一定時間たったら弾を消す
        Destroy(bullet, 5);
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
