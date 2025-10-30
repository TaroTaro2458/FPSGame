using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GetItem : MonoBehaviour
{
    // Inspectorで武器の設定、武器ごとにPrefabをアタッチ
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // 装備位置
    [SerializeField] Transform handTransform;
    // 最大装備数
    [SerializeField] int maxItems = 2;
    private List<GameObject> currentItems = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject item = other.GetComponent<GameObject>();
        // アイテム取得判定 tagで識別
        if (other.CompareTag("fullauto")&& AddItem(item))
        {
            Debug.Log("fullauto");   
            EquipItem(fullAutogunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("shotgun") && AddItem(item))
        {
            Debug.Log("shotgun");
            EquipItem(shotgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("single") && AddItem(item))
        {
            Debug.Log("handgun");
            EquipItem(handgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
    }

    void EquipItem(GameObject itemPrefab)
    {
        Transform equipSlot = handTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }

    public bool AddItem(GameObject item)
    {
        if (currentItems.Count >= maxItems)
        {
            Debug.Log("インベントリがいっぱいです！");
            return false;
        }
        currentItems.Add(item);
        return true;
    }
}
