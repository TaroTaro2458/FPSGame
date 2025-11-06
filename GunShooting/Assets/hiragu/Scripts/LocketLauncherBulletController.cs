using UnityEngine;

public class LocketLauncherBulletController : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;                // 爆発範囲
    [SerializeField] int damage = 50;                           // ダメージ量
    [SerializeField] float lifeTime = 5f;                       // 自壊までの時間
    [SerializeField] ParticleSystem explosionParticle;          // 爆発のエフェクト
    [SerializeField] float bulletSpeed = 5.0f;                  // 弾速

    Transform player;
    Transform shootingPoint;
    Rigidbody rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(
                (player.position - shootingPoint.position).normalized) * Quaternion.Euler(90, 0, 0);
        rb.linearVelocity = (player.position - shootingPoint.position).normalized * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            Explode();
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Playerタグのついたオブジェクトのみ判定
            if (nearbyObject.CompareTag("Player"))
            {
                // PlayerHealthスクリプトを探す（または任意のダメージスクリプト）
                PlayerHelth playerHealth = nearbyObject.GetComponent<PlayerHelth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    public void setShootingPoint(Transform transform)
    {
        shootingPoint = transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
