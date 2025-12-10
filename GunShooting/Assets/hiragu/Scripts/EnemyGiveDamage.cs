using UnityEngine;

public class EnemyGiveDamage : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] int damage = 100;

    // íeÇîÚÇŒÇµÇƒÇ≠ÇÈìGóp
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            return;
        }

        if (!other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }

        


    }

    // í«ê’Ç∑ÇÈìGóp
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
