using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilitySelectionUI : MonoBehaviour
{
    public GameObject panel;
    public AbilityButton[] abilityButtons; // 3つのボタン用

    // すべての能力データを保持(Resourcesフォルダからロードする場合)
    //private List<AbilityData> allAbilities;

    //手動で追加
    [SerializeField] List<AbilityData> allAbilities = new List<AbilityData>();

    void Start()
    {
        // ResourcesフォルダからすべてのAbilityDataをロード （なんかできない）
        //allAbilities = new List<AbilityData>(Resources.LoadAll<AbilityData>("Abilities"));
    }

    // 3つのランダムな能力を表示
    public void ShowRandomAbilities()
    {
        //デバック用
        Debug.Log("ShowRandomAbilities called"); 
        if (allAbilities == null || allAbilities.Count == 0) 
        { 
            Debug.LogError("Ability list is null or empty!"); 
            return; 
        }
        if (abilityButtons == null || abilityButtons.Length == 0) 
        { 
            Debug.LogError("Ability buttons not set!");
            return;
        }


        panel.SetActive(true);
        var selected = new List<AbilityData>();

        while (selected.Count < 3)
        {
            var pick = allAbilities[Random.Range(0, allAbilities.Count)];
            if (!selected.Contains(pick)) selected.Add(pick);
        }

        for (int i = 0; i < abilityButtons.Length; i++)
        {
            abilityButtons[i].SetAbility(selected[i]);
        }
    }

    // プレイヤーが能力を選択したときに呼ばれる
    public void OnAbilitySelected(AbilityData chosen)
    {
        panel.SetActive(false);
        ApplyAbility(chosen);
    }

    void ApplyAbility(AbilityData ability)
    {
        // プレイヤーに能力を付与する処理を書く
        Debug.Log("Selected: " + ability.abilityName);
        // 例：PlayerStats.Instance.Apply(ability);
    }
}
