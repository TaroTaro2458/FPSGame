using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingBullet : MonoBehaviour
{
    Transform targetPosition;
    [HideInInspector] public float bulletSpeed;              // 弾速
    [SerializeField] float rotateSpeed = 5f;        // 方向転換の速度
    [SerializeField] float lifeTime = 5f;           // 自然消滅の時間
    [SerializeField] int damage = 50;

    Rigidbody rb;
    PlayerHelth playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, lifeTime);
        rb = GetComponent<Rigidbody>();

        if (targetPosition != null)
        {
            transform.LookAt(targetPosition);
        }
            
    }

    void FixedUpdate()
    {
        if (targetPosition == null) return;

        Vector3 direction = (targetPosition.position - transform.position).normalized;

        Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotateAmount * rotateSpeed;

        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") &&
    !       other.CompareTag("ground") &&
            !other.CompareTag("EnemyBullet"))
        {
            if (other.CompareTag("Player"))
            {
                playerHealth = other.GetComponent<PlayerHelth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
