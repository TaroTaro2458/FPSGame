using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    // �ő�̗�
    [SerializeField] int maxHealth = 100;
    // ���݂̗̑�
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public int MaxHealth => maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         currentHealth = maxHealth;
    }

   public void TakeDamage(int damage)
    {
        // �_���[�W���󂯂�
        currentHealth -= damage;

        //HP��0�ȉ��ɂȂ����玀�S����
        
        if (currentHealth <= 0)
        {
            Die();
        }
        

    }

    void Die()
    {
        // ���S�����i�A�j���[�V�����A�Q�[���I�[�o�[�Ȃǁj
        Debug.Log("�v���C���[���S");
        Destroy(gameObject); 
    }
}
