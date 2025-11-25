using NUnit.Framework.Interfaces;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{

    //public GameObject item;
    private string itemName;
    private ItemData itemData;
    private GetItem getItem;
    // アイコンとテキストの参照
    public  Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    void Start()
    {
        
        Transform iconTransform = transform.Find("ItemIcon"); // 子オブジェクト名

        if (iconTransform != null)
        {
            // 子オブジェクトの Image コンポーネントを探す
            Image childImage = GetComponentInChildren<Image>();

            if (childImage != null && itemData != null && itemData.itemImage != null)
            {
                childImage.sprite = itemData.itemImage;
                Debug.Log("設定したスプライト名: " + itemData.itemImage.name);
            }
            else
            {
                Debug.LogWarning("画像の設定に失敗した");
            }
        }
    }

    // スロットの初期化
    public void Initialize(ItemData data, GetItem itemManager)
    {
        //クラスの変数に値を代入
        itemData = data;
        getItem = itemManager;
        itemName = itemData.itemName;
        GetComponentInChildren<TextMeshProUGUI>().text = itemName;

        if (itemNameText != null)
            itemNameText.text = itemData.itemName;

        if (itemIconImage != null && itemData.itemImage != null)
            itemIconImage.sprite = itemData.itemImage;

        // ボタンにクリックイベントを追加
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // スロットがクリックされたときの処理
    void OnClick()
    {
        getItem.RemoveItem(itemName);// インベントリから削除
        getItem.UnequipItem(itemName); // 装備中のアイテムも削除！
        Destroy(gameObject); // スロット自体を削除

    }
}
