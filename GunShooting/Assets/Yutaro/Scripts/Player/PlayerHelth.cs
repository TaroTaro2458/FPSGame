using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // 最大体力
    [SerializeField] int maxHealth = 100;
    // 現在の体力
    private int currentHealth;
    // 自動回復待ち時間
    [SerializeField] float healTimer;
    // 自動回復量/秒
    private int healRate = 5;
    [SerializeField] float healInterval = 5.0f; // 回復間隔
    // 自動回復上限
    [SerializeField] int autohealCap = 40;

    public int CurrentHealth => currentHealth;

    public int MaxHealth => maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         currentHealth = maxHealth;
    }

    void Update()
    {
        // ダメージ後の待ち時間をカウントダウン
        if (healTimer > 0f)
        {
            healTimer -= Time.deltaTime;
        }
        else
        {
            AutoHeal();
        }
    }

    public void TakeDamage(int damage)
    {
        // ダメージを受けたら回復までの待ち時間をリセット
        healTimer = healInterval;
        // ダメージを受ける
        currentHealth -= damage;
        // カメラシェイクを呼び出す
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.TriggerShake();
        }

        AudioManager.Instance.PlaySE3D(SEType.PlayerHit, transform.position); // ダメージを受けた時のSE

        //HPが0以下になったら死亡処理

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        

    }

    // 体力を回復する
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void AutoHeal()
    {
        // autohealCap以上なら何もしない
        if (currentHealth >= autohealCap) return;
        //HPが0以下なら何もしない
        if (currentHealth <= 0) return;

        float healAmount = healRate * Time.deltaTime*5;
        currentHealth += Mathf.RoundToInt(healAmount);

        if (currentHealth > autohealCap)
        {
            currentHealth = autohealCap;
        }

    }

    // 最大体力を上げる
    public void HPUp(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
    }


    void Die()
    {
        // 死亡処理 シーンをGameOverに切り替える
        Debug.Log("プレイヤー死亡");
        SceneManager.LoadScene("GameOver");
    }
}
