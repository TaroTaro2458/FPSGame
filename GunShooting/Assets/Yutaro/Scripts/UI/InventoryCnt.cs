using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //getItemスクリプトの参照
    [SerializeField] GetItem getItem;
    // インベントリスロットの親オブジェクト
    [SerializeField] Transform slotParent;
    // インベントリスロットのプレハブ
    [SerializeField] GameObject weaponSlotPrefab;
    // インベントリキャンバス
    [SerializeField] GameObject inventoryCanvas;

    // インベントリスロットのインスタンスリスト
    private List<GameObject> slotInstances = new List<GameObject>();

    void OnEnable()
    {
        getItem.onItemChanged.AddListener(UpdateInventoryDisplay);
    }

    //インベントリ画面が閉じられたときにリスナーを解除
    void OnDisable()
    {
        getItem.onItemChanged.RemoveListener(UpdateInventoryDisplay);
    }

    // インベントリ表示の更新
    public void UpdateInventoryDisplay()
    {
        foreach (GameObject slot in slotInstances)
            Destroy(slot);
        slotInstances.Clear();

        foreach (string item in getItem.GetCurrentItems())
        {
            GameObject slot = Instantiate(weaponSlotPrefab, slotParent);
            slot.GetComponentInChildren<Text>().text = item;
            slotInstances.Add(slot);
        }
    }

    // インベントリの表示/非表示切り替え
    public void ToggleInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}