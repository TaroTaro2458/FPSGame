using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("プレイヤー参照")]
    public PlayerHelth player;

    [Header("UI要素")]
    public Image hpFill;               // 赤いHPバー
    public TextMeshProUGUI hpText;     // HP表示テキスト

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerHelth>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float ratio = (float)player.CurrentHealth / player.MaxHealth;
        hpFill.fillAmount = ratio;

        if (hpText != null)
        {
            hpText.text = $"HP: {player.CurrentHealth} / {player.MaxHealth}";
        }
    }
}
