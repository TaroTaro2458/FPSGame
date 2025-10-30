﻿using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHp = 50;
    int currentHp;

    // 銃をドロップする
    [System.Serializable]
    [SerializeField] private class dropItem
    {
        public GameObject gunPrefab;
        //[Range(0f, 1f)] public float dropChance;
    }

    [SerializeField, Range(0f, 1f)] float overallDropChance = 1f;
    [SerializeField] List<dropItem> dropItems;

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
            EnemyDie();
        }
    }

    public void EnemyTakeDamge(int amount)
    {
        
        currentHp -= amount;
        Debug.Log(currentHp);
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
