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

    void ApplyAbility(AbilityData data)
    {
        switch (data.type)
        {
            case AbilityType.IncreaseHealth:
                // 体力を増やす処理
                break;
            case AbilityType.IncreaseSpeed:
                // 移動速度を上げる処理
                break;
            case AbilityType.FireResistance:
                // 火耐性を付与する処理
                break;
        }
    }
}
