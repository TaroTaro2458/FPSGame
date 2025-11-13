using System.Collections.Generic;
using TMPro;
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

    void Start()
    {
        //最初はインベントリ非表示
        inventoryCanvas.SetActive(false);
    }

    //Tabキーでインベントリの表示/非表示を切り替え
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    //インベントリ画面が開かれたときにリスナーを追加
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
        // 既存のスロットを削除
        foreach (GameObject slot in slotInstances)
            Destroy(slot);
        slotInstances.Clear();

        // 取得したアイテムをインベントリに表示
        /*foreach (GameObject item in getItem.GetCurrentItemObjects())
        {
            GameObject slot = Instantiate(weaponSlotPrefab, slotParent);
            slot.GetComponent<WeaponSlot>().Initialize(item, getItem);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.tag;
            slotInstances.Add(slot);

            RectTransform rect = slot.GetComponent<RectTransform>();
            rect.anchoredPosition += new Vector2(10f, -20f);
        }*/
        // string型でアイテム名を取得して表示
        foreach (string itemName in getItem.GetCurrentItems())
        {
            GameObject slot = Instantiate(weaponSlotPrefab, slotParent);
            slot.GetComponent<WeaponSlot>().Initialize(itemName, getItem);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
            slotInstances.Add(slot);
        }
    }

    // インベントリの表示/非表示切り替え
    public void ToggleInventory()
    {
        bool isActive = !inventoryCanvas.activeSelf;
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        // マウスカーソルの表示切り替え
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}