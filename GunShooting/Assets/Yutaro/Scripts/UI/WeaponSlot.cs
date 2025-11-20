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
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    // スロットの初期化
    public void Initialize(ItemData data, GetItem itemManager)
    {
        //クラスの変数に値を代入
        itemData = data;
        getItem = itemManager;
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
        getItem.RemoveItem(itemName);
        Destroy(gameObject); // スロット自体を削除
    }
}
