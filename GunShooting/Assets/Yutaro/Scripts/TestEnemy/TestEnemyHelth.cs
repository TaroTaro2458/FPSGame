using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("�G��HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���S�G�t�F�N�g��X�R�A���Z�Ȃǂ������ɒǉ��\
        Destroy(gameObject); // �G�I�u�W�F�N�g���폜
    }
}

