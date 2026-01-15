using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image icon;
    private AbilityData ability;
    private AbilitySelectionUI selectionUI;

    public void Initialize(AbilitySelectionUI ui)
    {
        selectionUI = ui;
    }

    public void SetAbility(AbilityData data)
    {
        ability = data;

        if (nameText == null) Debug.LogError("nameText is null!");
        if (descriptionText == null) Debug.LogError("descriptionText is null!");
        if (icon == null) Debug.LogError("icon is null!");

        nameText.text = data.abilityName;
        descriptionText.text = data.description;
        icon.sprite = data.icon;
    }


    public void OnClick()
    {

        selectionUI.OnAbilitySelected(ability);
    }

}
