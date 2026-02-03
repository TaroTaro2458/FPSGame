using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class AbilitySelectionUI : MonoBehaviour
{
    public GameObject panel;
    public AbilityButton[] abilityButtons; // ボタン用
    public static bool isUIOpen;

    [SerializeField] AbilityData Ability1;
    [SerializeField] AbilityData Ability2;
    //[SerializeField] AbilityData Ability3;


    void Awake()
    {
        panel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER HIT by " + other.name);
    }

    // 固定の能力を表示するメソッド
    public void ShowFixedAbilities()
    {
        isUIOpen = true;

        Debug.Log("Showing Fixed Abilities");

        foreach (var button in abilityButtons)
        {
            button.Initialize(this);
        }
        // パネル表示
        panel.SetActive(true);
        Debug.Log("Panel active: " + panel.activeSelf);

        // マウスカーソルの表示切り替え
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // ゲームの時間を止める
        Time.timeScale = 0f;

        abilityButtons[0].SetAbility(Ability1);
        abilityButtons[1].SetAbility(Ability2);
        //abilityButtons[2].SetAbility(Ability3);
        
        
    }


    // プレイヤーが能力を選択したときに呼ばれる
    public void OnAbilitySelected(AbilityData chosen)
    {
        isUIOpen = false;

        Debug.Log("Ability Selected: " + chosen.abilityName);
        panel.SetActive(false);
        ApplyAbility(chosen);

        // ゲームを再開
        Time.timeScale = 1f;

        // カーソルを非表示＆ロック
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        

    }


    void ApplyAbility(AbilityData ability)
    {
        // プレイヤーに能力を付与する処理を書く
        Debug.Log("Selected: " + ability.abilityName);

        //  体力を増加させる能力の場合
        if (ability.abilityName == "HP UP")
        {

            var player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.HPUp(20); // 体力を20回復
                Debug.Log("Player HP UP");
            }
            else
            {
                Debug.LogError("PlayerHealth component not found!");
            }
        }

        if(ability.abilityName == "Inventory UP")
        {
            var inventory = FindObjectOfType<GetItem>();
            if (inventory != null)
            {
                inventory.InventoryUp(1); // インベントリのスロット数を1増加
                Debug.Log("Inventory Expanded");
            }
            else
            {
                Debug.LogError("Inventory component not found!");
            }
        }
    }
}
