using UnityEngine;

public class BulletCnt : MonoBehaviour
{
    // 敵の体力スクリプトの参照
    EnemyHealth enemyHealth;
    // それぞれの武器からダメージ量を取得する
    [HideInInspector] public int playerBulletDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //５秒後に自動的に消滅
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ( other.gameObject.CompareTag("ground"))
        {
            
            //衝突したら即座に消滅
            Destroy(gameObject);
        }

        // 敵に当たった時の処理
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
