using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
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
        nameText.text = data.abilityName;
        descriptionText.text = data.description;
        icon.sprite = data.icon;
    }

    public void OnClick()
    {
        selectionUI.OnAbilitySelected(ability);
    }
}
