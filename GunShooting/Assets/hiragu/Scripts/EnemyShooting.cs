using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingInterval = 5.0f;
    [SerializeField] float bulletSpeed = 10.0f;

    GameObject bullet;
    Rigidbody bulletRb;
    Transform player;
    Rigidbody playerRb;
    Vector3 enemyDirectionControl;
    Vector3 bulletDirection;
    float countTime = 0;
    bool bossCheck = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossCheck = false;
        player = GameObject.FindWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody>();
        if (gameObject.name == "BossEnemy")
        {
            bossCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 常にプレイヤーの方を向く処理
        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        //  一定時間経過で射撃
        countTime += Time.deltaTime;
        if (countTime > shootingInterval && !bossCheck)
        {
            Shooting();

            countTime = 0;
        }

        if (countTime > shootingInterval && bossCheck)
        {
            ShootWithPrediction();

            countTime = 0;
        }
    }

    void ShootWithPrediction()
    {
        if (player == null || playerRb == null) return;

        Vector3 playerPos = player.position;
        Vector3 playerVel = playerRb.linearVelocity; // Rigidbodyの速度
        Vector3 enemyPos = shootingPoint.position;

        // 偏差撃ち計算
        Vector3 toPlayer = playerPos - enemyPos;
        float distance = toPlayer.magnitude;

        float a = Vector3.Dot(playerVel, playerVel) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector3.Dot(toPlayer, playerVel);
        float c = Vector3.Dot(toPlayer, toPlayer);

        // 解の公式で到達時間を計算
        float det = b * b - 4 * a * c;

        if (det < 0)
        {
            // 当たらない場合 → 現在位置を狙う
            BossShooting(toPlayer.normalized);
            return;
        }

        float t1 = (-b + Mathf.Sqrt(det)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(det)) / (2 * a);
        float t = Mathf.Min(t1, t2);

        if (t < 0) t = Mathf.Max(t1, t2); // 正の時間を選ぶ
        if (t <= 0)
        {
            BossShooting(toPlayer.normalized);
            return;
        }

        // 予測位置
        Vector3 predictedPos = playerPos + playerVel * t;
        Vector3 aimDir = (predictedPos - enemyPos).normalized;

        BossShooting(aimDir);
    }

    void BossShooting(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.LookRotation(dir));
        bullet.transform.rotation = Quaternion.LookRotation(
            (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = dir * bulletSpeed;
        Destroy(bullet, 5);
    }


    void Shooting()
    {
        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(
            (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletDirection = (player.position - shootingPoint.position).normalized;
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;
        Destroy(bullet, 5);
    }
}
