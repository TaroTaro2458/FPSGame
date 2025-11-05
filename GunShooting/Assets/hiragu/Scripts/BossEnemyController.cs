using UnityEngine;
using UnityEngine.AI;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] float stopDistance = 10f;              // ˆê’è‹——£‚Ü‚Å‹ß‚Ã‚­
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootingInterval = 3.0f;
    [SerializeField] float bulletSpeed = 5.0f; 

    Transform player;
    NavMeshAgent agent;
    float distance;

    GameObject bullet;
    Rigidbody bulletRb;
    Vector3 enemyDirectionControl;
    Vector3 bulletDirection;
    float countTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }

        enemyDirectionControl = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(enemyDirectionControl);

        countTime += Time.deltaTime;
        if (countTime > shootingInterval)
        {
            bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
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
