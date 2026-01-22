using System.ComponentModel;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class pursuitShootingEnemyController : MonoBehaviour, IEnemyDeathListener
{
    [SerializeField] GameObject bulletPrefab;                 // 弾のプレハブ
    [SerializeField] float range = 15f;                 // 射程・この距離以内にプレイヤーがいなかった移動する
    [SerializeField] float interval = 5f;               // 射撃間隔
    float intervalCount = 0;                            // 射撃間隔を計測する
    [SerializeField] float bulletSpeed = 10f;           // 弾の速度
    [SerializeField] Transform shootingPoint;
    [SerializeField] float destroyTime = 5;

    NavMeshAgent nav;                                   // プレイヤーを追跡する用のnavMesh
    Transform player;                                   // プレイヤーの子オブジェクトにある狙う位置を取得するため
    Transform targetPoint;                              // 狙う位置
    float distance;                                     // プレイヤーとの距離
    Rigidbody bulletRb;
    Vector3 bulletDirection;
    GameObject bullet;
    //bool isFootstepPlaying;
    [SerializeField] float footstepInterval = 0.5f;
    float footstepTimer;

    Animator anim;
    bool isDie = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetPoint = player.Find("AimPoint");
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();


        nav.stoppingDistance = range;   // ★これが超重要
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) return;
        if (player == null) return;

        distance = Vector3.Distance(transform.position, targetPoint.position);

        // 射程外 → 追跡
        if (distance > range)
        {
            Move();
            return;
        }

        // 射程内だが見えない → 追跡
        if (!CanSeePlayer())
        {
            Move();
            return;
        }

        // ===== ここから射撃状態 =====
        StopAndShoot();
    }

    void Move()
    {
        nav.updateRotation = true;
        nav.isStopped = false;
        nav.SetDestination(player.position);

        // ★アニメーション制御
        anim.SetBool("Walk", true);
        anim.SetBool("Shooting", false);

        
       

        intervalCount = 0f;
    }


    // rayを飛ばしてプレイヤーまでの射線上に何もないことを確認する
    bool CanSeePlayer()
    {
        Vector3 dir = (targetPoint.position - shootingPoint.position).normalized;

        // RaycastHitで最初に当たったものをチェック
        if (Physics.Raycast(shootingPoint.position, dir, out RaycastHit hit, range))
        {

            // 以下4行はrayを可視化するためのもの
            Vector3 origin = shootingPoint.position;
            Vector3 dira = (targetPoint.position - origin).normalized;
            float distance = range;
            Debug.DrawRay(origin, dira * distance, Color.green);

            if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("EnemyBullet"))
            {
                return true; // 射線に遮蔽物なし
            }
        }
        return false; // 何か壁などに当たった
    }

    // 弾のスピードを渡してプレイヤーの向けて撃つ
    void Shooting()
    {
        if (player == null) return;                                                                     // エラー対策

        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);             // 弾を生成
        bullet.transform.rotation = Quaternion.LookRotation(
            (targetPoint.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);        // 弾がカプセルの場合向きを合わせる
        bulletRb = bullet.GetComponent<Rigidbody>();                                                    // 弾にスピードを渡すためのrigidbody
        bulletDirection = (targetPoint.position - shootingPoint.position).normalized;                        // プレイヤーの現在位置
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;                                        // プレイヤーに向かって移動させる
        Destroy(bullet, destroyTime);                                                                   // 一定時間経過で壊す
    }

    void StopAndShoot()
    {
        // 完全停止
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        nav.ResetPath();
        nav.updateRotation = false;

        // ★移動アニメ完全停止
        anim.SetBool("Walk", false);

        // プレイヤーの方を向く
        Vector3 lookPos = targetPoint.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        intervalCount += Time.deltaTime;
        if (intervalCount >= interval)
        {
            anim.SetBool("Shooting", true);   // ★射撃開始
            Shooting();

            Invoke(nameof(EndShooting), 2f); // アニメ長に合わせる
            intervalCount = 0f;
        }
    }

    public void OnDeath()
    {
        isDie = true;

        if (nav != null && nav.isActiveAndEnabled && nav.isOnNavMesh)
        {
            nav.isStopped = true;
            nav.ResetPath();
        }

        anim.SetTrigger("Die");
        Debug.Log("アニメーション再生");
        nav.enabled = false;
        Destroy(gameObject, 0.8f);
    }

    void EndShooting()
    {
        anim.SetBool("Shooting", false);
    }
}
