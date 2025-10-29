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
    Vector3 enemyDirectionControl;
    Vector3 bulletDirection;
    float countTime = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 常にプレイヤーの方を向く処理
        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        //  一定時間経過で射撃
        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            bullet =  Instantiate(bulletPrefab,shootingPoint.position, shootingPoint.rotation);
            bullet.transform.rotation = Quaternion.LookRotation(
                (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
            bulletRb = bullet.GetComponent<Rigidbody>();
            bulletDirection = (player.position - shootingPoint.position).normalized;
            bulletRb.linearVelocity = bulletDirection * bulletSpeed;
            Destroy(bullet, 5);

            countTime = 0;
        }
    }
}
