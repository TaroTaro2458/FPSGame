using UnityEngine;

public class BulletCnt : MonoBehaviour
{
    EnemyHealth enemyHealth;
    // ‚»‚ê‚¼‚ê‚Ì•Ší‚©‚çƒ_ƒ[ƒW—Ê‚ğæ“¾‚·‚é
    [HideInInspector] public int playerBulletDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //‚T•bŒã‚É©“®“I‚ÉÁ–Å
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ( other.gameObject.CompareTag("ground"))
        {
            
            //Õ“Ë‚µ‚½‚ç‘¦À‚ÉÁ–Å
            Destroy(gameObject);
        }

        // “G‚É“–‚½‚Á‚½‚Ìˆ—
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyTakeDamge(playerBulletDamage);
                Destroy(gameObject);
            }
        }
    }

}
