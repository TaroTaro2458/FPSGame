
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GetItem : MonoBehaviour
{
    // Inspectorで武器の設定、武器ごとにPrefabをアタッチ
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // アタッチメントPrefab
    [SerializeField] GameObject cooldownAttachmentPrefab;
    [SerializeField] GameObject maxHeatAttachmentPrefab;
    // 装備位置　武器ごとで別の位置
    [SerializeField] Transform fullautoHandTransform;
    [SerializeField] Transform shotgunHandTransform;
    [SerializeField] Transform handgunHandTransform;
    // 装備位置　アタッチメント系
    [SerializeField] Transform cooldownAttachmentTransform;
    [SerializeField] Transform maxHeatAttachmentTransform;
    // 最大装備数
    [SerializeField] int maxItems;
    // 最大装備数プロパティ(InventoryCount用)
    public int MaxItems => maxItems;
    //UIの参照
    [SerializeField]  InventoryCount inventoryCount;

    // 所持アイテムリスト
    private List<GameObject> currentItems = new List<GameObject>();
    private List<string> currentItemList = new List<string>();
    // アイテム変更イベント
    public UnityEvent onItemChanged = new UnityEvent();
    // すべてのアイテムデータリスト
    [SerializeField] private List<ItemData> allItemDataList;
    // アイテムデータ辞書
    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();
    // Overheatスクリプトの参照
    [SerializeField] Overheat overheat;
    
    private int fireGageCount = 0;

    void Awake()
    {
        // アイテムデータ辞書の初期化
        foreach (ItemData data in allItemDataList)
        {
            if (!itemDataDict.ContainsKey(data.itemName))
            {
                itemDataDict[data.itemName] = data;
            }
        }
    }
    // アイテム取得判定
    void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
        // アイテム取得判定 tagで識別
        if (other.CompareTag("fullauto") && AddItem(item))
        {
            Debug.Log("fullauto");
            GetFullauto(fullAutogunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("shotgun") && AddItem(item))
        {
            Debug.Log("shotgun");
            GetShotgun(shotgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("single") && AddItem(item))
        {
            Debug.Log("handgun");
            GetHandgun(handgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("CoolingAttachment") && AddItem(item))
        {
            Debug.Log("冷却アタッチメント取得");
            ApplyCooldownAttachment(cooldownAttachmentPrefab); 
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("MaxHeatAttachment") && AddItem(item))
        {
            Debug.Log("拡張アタッチメント取得");
            ApplyMaxHeatAttachment(maxHeatAttachmentPrefab);
            Destroy(other.gameObject);
        }
    }

    // アイテム装備処理
    void GetFullauto(GameObject itemPrefab)
    {
        Transform equipSlot = fullautoHandTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }
    void GetShotgun(GameObject itemPrefab)
    {
        Transform equipSlot = shotgunHandTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }
    void GetHandgun(GameObject itemPrefab)
    {
        Transform equipSlot = handgunHandTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }

    // アタッチメント適用処理(冷却氷)
    void ApplyCooldownAttachment(GameObject itemPrefab)
    {
        ItemData data = itemPrefab.GetComponent<ItemData>();
        if (data != null && data.itemType == ItemType.Attachment && data.coolingAttachment != null)
        {
            // 現在の装備数を取得
            int count = cooldownAttachmentTransform.childCount;
            // オフセット値（上方向にずらす距離）
            float offsetY = 0.08f; // 必要に応じて調整

            // 新しい位置を計算
            Vector3 offsetPosition = cooldownAttachmentTransform.position + new Vector3(0f, 0f, offsetY * count);

            // インスタンス生成(装備）
            GameObject equippedItem = Instantiate(itemPrefab, offsetPosition, cooldownAttachmentTransform.rotation, cooldownAttachmentTransform);
            overheat.AddCooldownAttachment(data.coolingAttachment);
        }
    }
    // アタッチメント適用処理(拡張マガジン)
    void ApplyMaxHeatAttachment(GameObject itemPrefab)
    {
        ItemData data = itemPrefab.GetComponent<ItemData>();
        if (data != null && data.itemType == ItemType.Attachment && data.maxHeatAttachment != null)
        {
            // 現在の装備数を取得
            int count = maxHeatAttachmentTransform.childCount;

            // オフセット値（右方向にずらす距離）
            float offsetZ = -0.05f; // 必要に応じて調整

            // 新しい位置を計算
            Vector3 offsetPosition = maxHeatAttachmentTransform.position + new Vector3( 0f, 0f,offsetZ * count);

            // インスタンス生成(装備）
            GameObject equippedItem = Instantiate(itemPrefab, offsetPosition, maxHeatAttachmentTransform.rotation, maxHeatAttachmentTransform);

            // 効果を適用
            overheat.AddMaxHeatAttachment(data.maxHeatAttachment);
        }
    }


    // アイテム削除処理
    public void UnequipItem(string itemName)
    {
        // 装備中のアイテムを探して削除(武器ごとに探す)
        foreach (Transform child in fullautoHandTransform)
        {
            ItemData data = child.GetComponent<ItemData>();
            if (data != null && data.itemName == itemName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
        foreach (Transform child in shotgunHandTransform)
        {
            ItemData data = child.GetComponent<ItemData>();
            if (data != null && data.itemName == itemName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
        foreach (Transform child in handgunHandTransform)
        {
            ItemData data = child.GetComponent<ItemData>();
            if (data != null && data.itemName == itemName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
        foreach (Transform child in cooldownAttachmentTransform)
        {
            ItemData data = child.GetComponent<ItemData>();
            if (data != null && data.itemName == itemName)
            {
                Destroy(child.gameObject);
                break;
            }
        }

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

    // アイテム名からItemDataを取得
    public ItemData GetItemData(string itemName)
    {
        // アイテムデータ辞書から取得
        if (itemDataDict.TryGetValue(itemName, out ItemData data))
        {
            return data;
        }
        Debug.LogWarning($"ItemData not found for itemName: {itemName}");
        return null;



    }


    public void InventoryUp(int amount)
    {
        maxItems += amount;
        inventoryCount.UpdateInventoryText();
    }


}
