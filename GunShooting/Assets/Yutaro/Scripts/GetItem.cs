using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    // 所持アイテムリスト
    private List<GameObject> currentItems = new List<GameObject>();
    private List<string> currentItemList = new List<string>();
    // アイテム変更イベント
    public UnityEvent onItemChanged = new UnityEvent();


    void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
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

    // アイテム装備処理
    void EquipItem(GameObject itemPrefab)
    {
        Transform equipSlot = handTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }

    // アイテムをインベントリに追加と判定
    public bool AddItem(GameObject item)
    {
        if (item == null) return false;
        if (currentItemList.Count >= maxItems) return false;

        ItemData data = item.GetComponent<ItemData>();
        if (data != null)
        {
            currentItemList.Add(data.itemName);
            onItemChanged.Invoke();// UIに通知
            Destroy(item); // 情報を保存したあとに削除
            return true;
        }
        /*currentItems.Add(item);
        currentItemList.Add(item.tag);
        onItemChanged.Invoke(); // UIに通知
        return true;*/
        Debug.LogWarning("ItemData が見つかりませんでした");
        return false;
    }

    // アイテムをインベントリから削除
    public void RemoveItem(string item)
    {
        // アイテムが存在するか確認
        if (currentItemList.Contains(item))
        {
            currentItemList.Remove(item);
            onItemChanged.Invoke();
        }
    }

    // 現在の所持アイテムを取得(string)
    public List<string> GetCurrentItems()
    {
        return currentItemList;
    }
    // 現在の所持アイテムを取得(GameObject)
    public List<GameObject> GetCurrentItemObjects()
    {
        return currentItems;
    }
}
