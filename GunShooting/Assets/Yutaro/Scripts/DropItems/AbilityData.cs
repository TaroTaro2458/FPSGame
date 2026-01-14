using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "BossDrop/Ability")]
public class AbilityData : ScriptableObject
{
    // 能力の名前
    public string abilityName;
    // 能力の説明
    public string description;
    // 能力のアイコン
    public Sprite icon;
    // 能力のタイプと値
    public AbilityType type;
    public float value;
}

public enum AbilityType
{
    IncreaseHealth,
    IncreaseSpeed,
    FireResistance,
    // 他にも自由に追加できるよ
}
