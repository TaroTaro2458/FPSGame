using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] int damage = 20;
    [SerializeField] float explosionRadius = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(particle, collision.transform.position, Quaternion.identity);
            Explode();
            Destroy(effect, 5.0f);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // PlayerHealthスクリプトを探す（または任意のダメージスクリプト）
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
