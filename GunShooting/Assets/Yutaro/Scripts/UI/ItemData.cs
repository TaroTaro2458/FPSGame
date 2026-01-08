using UnityEngine;

// アイテムの種類
public enum ItemType
{
    Weapon,
    Attachment
}

//[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : MonoBehaviour
{
   

    // アイテム名
    public string itemName;

    // アイテムの画像
    public Sprite itemImage;

    // アイテムの種類
    public ItemType itemType;

    // アタッチメント用
    public CoolingAttachment coolingAttachment;
}
