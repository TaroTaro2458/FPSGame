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

    NavMeshAgent nav;                                   // プレイヤーを追跡する用のnavMesh
    Transform player;                                   // プレイヤーの子オブジェクトにある狙う位置を取得するため
    Transform targetPoint;                              // 狙う位置
    float distance;                                     // プレイヤーとの距離
    Rigidbody bulletRb;
    Vector3 bulletDirection;
    GameObject bullet;
    bool isFootstepPlaying;
    [SerializeField] float footstepInterval = 0.5f;
    float footstepTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetPoint = player.Find("AimPoint");
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーとの距離を取得
        distance = distance = Vector3.Distance(transform.position, targetPoint.position);
        

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
                    Vector3 targetPos = targetPoint.position;
                    targetPos.y = transform.position.y;         // y軸方向まで見る必要はない
                    transform.LookAt(targetPos);                   // プレイヤーのほうを向く
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
            nav.isStopped = false;
            nav.SetDestination(player.position);

            // 移動中か？
            if (nav.velocity.magnitude > 0.1f)
            {
                footstepTimer += Time.deltaTime;

                if (footstepTimer >= footstepInterval)
                {
                    AudioManager.Instance.PlaySE3D(SEType.EnemyWalk, transform.position);
                    footstepTimer = 0f;
                }
            }
            else
            {
                // 止まったらタイマーをリセット
                footstepTimer = 0f;
            }

            intervalCount = interval - 0.5f;
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
                (targetPoint.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);        // 弾がカプセルの場合向きを合わせる
            bulletRb = bullet.GetComponent<Rigidbody>();                                                    // 弾にスピードを渡すためのrigidbody
            bulletDirection = (targetPoint.position - shootingPoint.position).normalized;                        // プレイヤーの現在位置
            bulletRb.linearVelocity = bulletDirection * bulletSpeed;                                        // プレイヤーに向かって移動させる
            Destroy(bullet, destroyTime);                                                                   // 一定時間経過で壊す
        }
    }
}
