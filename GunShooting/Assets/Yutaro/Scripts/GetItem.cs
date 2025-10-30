using System;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    // Inspectorで武器の設定、武器ごとにPrefabを作成
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // 装備位置
    [SerializeField] Transform handTransform; 

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
        // アイテム取得判定 tagで識別
        if (other.CompareTag("fullauto"))
        {
            Debug.Log("fullauto");
            EquipItem(fullAutogunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("shotgun"))
        {
            Debug.Log("shotgun");
            EquipItem(shotgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("single"))
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
}
