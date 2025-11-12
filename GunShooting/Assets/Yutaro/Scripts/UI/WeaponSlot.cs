using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{

    //public GameObject item;
    private string itemName;
    private GetItem getItem;

    // スロットの初期化
    public void Initialize(string name, GetItem itemManager)
    {
        //クラスの変数に値を代入
        itemName = name;
        getItem = itemManager;
        GetComponentInChildren<TextMeshProUGUI>().text = itemName;

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
