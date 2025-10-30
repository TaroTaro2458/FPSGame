using UnityEngine;

public class EnemyGiveDamage : MonoBehaviour
{
    PlayerHelth playerHealth;
    [SerializeField] int damage = 100;

    // �e���΂��Ă���G�p
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth = other.gameObject.GetComponent<PlayerHelth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }

        

        
    }

    // �ǐՂ���G�p
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHelth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
