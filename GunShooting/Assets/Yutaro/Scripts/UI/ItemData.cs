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

    // アイテム識別番号
    public int itemID;

    // アイテムの重量
    public int weight;

    // アタッチメント用
    public CoolingAttachment coolingAttachment;
    public MaxHeatAttachment maxHeatAttachment;

}
