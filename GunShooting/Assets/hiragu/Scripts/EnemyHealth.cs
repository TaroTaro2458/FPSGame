using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHp = 50;
    int currentHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyTakeDamge(int amount)
    {
        currentHp -= amount;
    }
}
