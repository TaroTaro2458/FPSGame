using UnityEngine;

public class LocketLauncherBulletController : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] float explosionRadius = 5f;     // 爆発範囲
    [SerializeField] int damage = 50;             // ダメージ量
    //[SerializeField] private GameObject explosionEffect;     // 爆発エフェクトのPrefab

    [Header("Projectile Settings")]
    [SerializeField] float lifeTime = 5f;            // 自壊までの時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            Explode();
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
