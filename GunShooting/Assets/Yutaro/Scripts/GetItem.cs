
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GetItem : MonoBehaviour
{
    // Inspectorで武器の設定、武器ごとにPrefabをアタッチ
    [SerializeField] GameObject fullAutogunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject handgunPrefab;
    // 装備位置　武器ごとで別の位置 頑張れ！
    [SerializeField] Transform fullautoHandTransform;
    //[SerializeField] Transform shotgunHandTransform;
    //[SerializeField] Transform handgunHandTransform;
    // 最大装備数
    [SerializeField] int maxItems = 2;
    // 所持アイテムリスト
    private List<GameObject> currentItems = new List<GameObject>();
    private List<string> currentItemList = new List<string>();
    // アイテム変更イベント
    public UnityEvent onItemChanged = new UnityEvent();
    // すべてのアイテムデータリスト
    [SerializeField] private List<ItemData> allItemDataList;
    // アイテムデータ辞書
    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();

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
        if (other.CompareTag("fullauto")&& AddItem(item))
        {
            Debug.Log("fullauto");   
            GetFullauto(fullAutogunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("shotgun") && AddItem(item))
        {
            Debug.Log("shotgun");
            GetFullauto(shotgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
        else if (other.CompareTag("single") && AddItem(item))
        {
            Debug.Log("handgun");
            GetFullauto(handgunPrefab); // 入手と同時に装備
            Destroy(other.gameObject); // アイテムを消す
        }
    }

    // アイテム装備処理
    void GetFullauto(GameObject itemPrefab)
    {
        Transform equipSlot = fullautoHandTransform; // 装備位置
        GameObject equippedItem = Instantiate(itemPrefab, equipSlot.position, equipSlot.rotation, equipSlot);
    }
    // アイテム削除処理
    public void UnequipItem(string itemName)
    {
        // 装備中のアイテムを探して削除
        foreach (Transform child in fullautoHandTransform)
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
}
