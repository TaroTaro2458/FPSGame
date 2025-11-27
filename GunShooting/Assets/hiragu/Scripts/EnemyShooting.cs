using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingInterval = 5.0f;
    [SerializeField] float bulletSpeed = 10.0f;

    Transform player;
    float countTime = 0;
    GameObject bullet;

    Rigidbody bulletRb;

    Vector3 enemyDirectionControl;
    Vector3 bulletDirection;

    [SerializeField] bool isBoss = false;
    bool isPinch = false;
    EnemyHealth enmeyHealth;

    [Header("ƒ{ƒX‚ÌHP‚ªŒ¸‚Á‚½Žž‚É‹­‰»‚·‚é")]
    [SerializeField] float pinchShootingInterval = 0.7f;
    [SerializeField] float pinchBulletSpeed = 13f;
    [SerializeField] int pinchHp = 100;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if(isBoss)
        {
            enmeyHealth = GetComponent<EnemyHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("‹­‚­‚È‚Á‚½");
        }
    }

    void Shooting()
    {
        if (player == null) return;

        bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.transform.rotation = Quaternion.LookRotation(
            (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletDirection = (player.position - shootingPoint.position).normalized;
        bulletRb.linearVelocity = bulletDirection * bulletSpeed;
        Destroy(bullet, 5);
    }

}
