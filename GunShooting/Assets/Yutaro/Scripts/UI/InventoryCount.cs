using UnityEngine;
using UnityEngine.UI;

public class InventoryCount : MonoBehaviour
{
    [SerializeField] Text inventoryText; // UIのText（TextMeshProならTMP_Textに変更）
    [SerializeField] GetItem getItem;    // GetItemスクリプトの参照

    void Start()
    {
        if (getItem == null)
        {
            getItem = FindObjectOfType<GetItem>();
        }

        // 最初に表示を更新
        UpdateInventoryText();

        // アイテム変更時に自動更新
        getItem.onItemChanged.AddListener(UpdateInventoryText);
    }

    // インベントリのテキストを更新するメソッド
    public void UpdateInventoryText()
    {
        int current = getItem.GetCurrentItems().Count;
        int max = getItem.MaxItems;
        inventoryText.text = $"Inventory: {current} / {max}";
    }

}
