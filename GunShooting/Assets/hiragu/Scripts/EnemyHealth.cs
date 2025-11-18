using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHp = 50;
    [HideInInspector] public int EnmeyCurrentHp;

    // 銃をドロップする
    [System.Serializable]
    [SerializeField] private class dropItem
    {
        public GameObject gunPrefab;
        //[Range(0f, 1f)] public float dropChance;
    }

    [SerializeField, Range(0f, 1f)] float overallDropChance = 1f;
    [SerializeField] List<dropItem> dropItems;

    bool isBoss = false;

    [Header("ボスの場合にHPが減ったことをUIに通知する")]
    [SerializeField] UnityEvent<int,int> onHealthChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnmeyCurrentHp = maxHp;
        if(name == "BossEnemy")
        {
            isBoss = true;
            onHealthChanged.Invoke(EnmeyCurrentHp,maxHp);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnmeyCurrentHp <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyTakeDamge(int amount)
    {

        EnmeyCurrentHp -= amount;
        if (isBoss)
        {
            onHealthChanged.Invoke(EnmeyCurrentHp, maxHp);
        }
        Debug.Log(EnmeyCurrentHp);
    }

    private void EnemyDie()
    {
        Debug.Log("敵死んだ");
        GunDrop();
        Destroy(gameObject);
    }

    private void GunDrop()
    {
        if (Random.value > overallDropChance)
        {
            // 落とさない
            Debug.Log("No item dropped.");
            return;
        }

        // 何も当たらなければドロップなし
        if (dropItems.Count == 0)
        {
            Debug.Log("Drop failed — no item matched individual chances.");
            return;
        }

        // 候補の中からランダムに1つドロップ
        if (dropItems.Count > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Count);
            Instantiate(dropItems[randomIndex].gunPrefab, transform.position, Quaternion.identity);
        }
    }
}
