using UnityEngine;

public class BulletCnt : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //‚T•bŒã‚É©“®“I‚ÉÁ–Å
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        //CompareTag("Enemy") ||
        // “G‚È‚Ç‚É“–‚½‚Á‚½‚Æ‚«‚¾‚¯ˆ—
        if ( other.gameObject.CompareTag("ground"))
        {
            
            //Õ“Ë‚µ‚½‚ç‘¦À‚ÉÁ–Å
            Destroy(gameObject);
        }

    }

}
