using UnityEngine;

public class BulletCnt : MonoBehaviour
{
    EnemyHealth enemyHealth;
    // ���ꂼ��̕��킩��_���[�W�ʂ��擾����
    [HideInInspector] public int playerBulletDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�T�b��Ɏ����I�ɏ���
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ( other.gameObject.CompareTag("ground"))
        {
            
            //�Փ˂����瑦���ɏ���
            Destroy(gameObject);
        }

        // �G�ɓ����������̏���
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
