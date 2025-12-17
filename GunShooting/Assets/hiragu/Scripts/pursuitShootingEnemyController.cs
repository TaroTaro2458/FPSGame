using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class pursuitShootingEnemyController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;                 // 弾のプレハブ
    [SerializeField] float range = 15f;                 // 射程・この距離以内にプレイヤーがいなかった移動する
    [SerializeField] float interval = 5f;               // 射撃間隔
    float intervalCount = 0;                            // 射撃間隔を計測する
    [SerializeField] float bulletSpeed = 10f;           // 弾の速度
    [SerializeField] Transform shootingPoint;
    [SerializeField] float destroyTime = 5;
    [SerializeField] private LayerMask hitMask; 

    NavMeshAgent nav;                                   // プレイヤーを追跡する用のnavMesh
    Transform player;                                   // プレイヤーの位置を取得する
    float distance;                                     // プレイヤーとの距離
    Rigidbody bulletRb;
    Vector3 bulletDirection;
    GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーとの距離を取得
        distance = Vector3.Distance(transform.position, player.position);

        if (player != null)
        {
            // プレイヤーが射程外なら追跡、そうでないなら射撃
            if (distance > range)
            {
                Move();
            }
            else
            {
                if (CanSeePlayer())
                {
                    transform.LookAt(player);                   // プレイヤーのほうを向く
                    nav.isStopped = true;                       // 止まるようにする
                    intervalCount += Time.deltaTime;            // カウントを進める
                    if (intervalCount >= interval)               // カウントが射撃間隔を越したら撃つ
                    {
                        Shooting();                             // 射撃のメソッド
                        intervalCount = 0f;                     // カウントをリセット
                    }
                }
                else
                {
                    Move();
                }

                
            }

        }

        void Move()
        {
            intervalCount = interval - 0.5f;            // 次に止まった場合にすぐに撃てるように調整
            nav.isStopped = false;                      // 止まらないようにする
            nav.SetDestination(player.position);        // プレイヤーを追跡
        }

        bool CanSeePlayer()
        {
            Vector3 dir = (player.position - transform.position).normalized;

            // RaycastHitで最初に当たったものをチェック
            if ((Physics.Raycast(
            shootingPoint.position,
            dir,
            out RaycastHit hit,
            range,
            hitMask)))
            {
                if (hit.transform.CompareTag("Player"))
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
                (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);        // 弾がカプセルの場合向きを合わせる
            bulletRb = bullet.GetComponent<Rigidbody>();                                                    // 弾にスピードを渡すためのrigidbody
            bulletDirection = (player.position - shootingPoint.position).normalized;                        // プレイヤーの現在位置
            bulletRb.linearVelocity = bulletDirection * bulletSpeed;                                        // プレイヤーに向かって移動させる
            Destroy(bullet, destroyTime);                                                                   // 一定時間経過で壊す
        }
    }
}
