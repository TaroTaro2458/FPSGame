using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("�v���C���[�Q��")]
    public PlayerHelth player;

    [Header("UI�v�f")]
    public Image hpFill;               // �Ԃ�HP�o�[
    public TextMeshProUGUI hpText;     // HP�\���e�L�X�g

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
