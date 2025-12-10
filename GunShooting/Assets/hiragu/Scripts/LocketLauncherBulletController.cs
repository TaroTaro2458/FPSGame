using UnityEngine;

public class LocketLauncherBulletController : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;                // 爆発範囲
    [SerializeField] int damage = 50;                           // ダメージ量
    [SerializeField] float lifeTime = 5f;                       // 自壊までの時間
    [SerializeField] GameObject explosionParticle;          // 爆発のエフェクト
    [HideInInspector] public float bulletSpeed;
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
        // ★ Transform が Destroy されていないかチェック
        if (player == null || shootingPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        // ★ 安全に座標を取得
        Vector3 direction = player.position - shootingPoint.position;

        // 方向がゼロベクトルにならないように保険
        if (direction.sqrMagnitude < 0.0001f)
            return;

        transform.rotation = Quaternion.LookRotation(direction.normalized) * Quaternion.Euler(90, 0, 0);

        rb.linearVelocity = (player.position - shootingPoint.position).normalized * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*!other.CompareTag("Enemy") &&*/ other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            /*Explode();
            GameObject effect = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(effect, 5.0f);*/
            return;
        }
        Explode();
        GameObject effect = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(effect, 5.0f);
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
                PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
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
