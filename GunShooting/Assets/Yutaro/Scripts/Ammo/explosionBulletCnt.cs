using UnityEngine;

public class explosionBulletCnt : MonoBehaviour
{
    // 敵の体力スクリプトの参照
    EnemyHealth enemyHealth;
    // 弾のダメージ
    [HideInInspector] public int playerBulletDamage;
    // 爆発範囲
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] GameObject explosionEffect; // 爆発エフェクト（任意）

    void Start()
    {
        Destroy(gameObject, 5f); // 時間経過で自動消滅
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    void Explode()
    {
        // 爆発エフェクトを再生
       /* if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }*/

        // 範囲内の敵を検出
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.EnemyTakeDamge(playerBulletDamage);
                }
            }
        }

        Destroy(gameObject);
    }

}

