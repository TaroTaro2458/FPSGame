using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    public AbilityData ability; // Inspectorで設定できるようにpublicにする

    void Start()
    {
        if (ability != null)
        {
            Debug.Log("使う能力: " + ability.abilityName);
            ApplyAbility(ability);
        }
        else
        {
            Debug.LogWarning("AbilityData が設定されていません！");
        }
    }

    // 能力を適用するメソッド
    void ApplyAbility(AbilityData data)
    {
        switch (data.type)
        {
            case AbilityType.IncreaseHealth:
                Debug.Log("体力が " + data.value + " 増加しました！");
                // 体力を増やす処理
                break;
            case AbilityType.InventoryUP:
                Debug.Log("インベントリのスロット数が " + data.value + " 増加しました！");
                // inventoryのスロット数を増やす処理
                break;
            /*case AbilityType.FireResistance:
                // 火耐性を付与する処理
                break;*/
        }
    }
}
