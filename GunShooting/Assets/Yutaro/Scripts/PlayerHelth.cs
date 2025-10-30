using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    // 最大体力
    [SerializeField] int maxHealth = 100;
    // 現在の体力
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
        // ダメージを受ける
        currentHealth -= damage;

        //HPが0以下になったら死亡処理
        
        if (currentHealth <= 0)
        {
            Die();
        }
        

    }

    void Die()
    {
        // 死亡処理（アニメーション、ゲームオーバーなど）
        Debug.Log("プレイヤー死亡");
        Destroy(gameObject); 
    }
}
