using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHelth playerHealth;
    [SerializeField] private Image hpFillImage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private RectTransform hpBarTransform;
    [SerializeField] private Image damageOverlay; // 🔴画面赤フェード

    private float previousFillAmount;
    private bool isLowHealthEffect = false;

    private void Start()
    {
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<PlayerHelth>();

        if (hpBarTransform == null && hpFillImage != null)
            hpBarTransform = hpFillImage.GetComponent<RectTransform>();

        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0); // 最初は透明

        previousFillAmount = 1f;
    }

    private void Update()
    {
        if (playerHealth == null || hpFillImage == null) return;

        float fill = Mathf.Clamp01((float)playerHealth.CurrentHealth / playerHealth.MaxHealth);

        // HPバー更新
        hpFillImage.fillAmount = fill;

        // 綺麗な赤→黄→緑補間
        Color targetColor;
        if (fill < 0.5f)
            targetColor = Color.Lerp(Color.red, Color.yellow, fill * 2f);
        else
            targetColor = Color.Lerp(Color.yellow, Color.green, (fill - 0.5f) * 2f);
        hpFillImage.color = targetColor;

        // HP低下時に演出発動
        if (fill <= 0.3f && !isLowHealthEffect)
        {
            isLowHealthEffect = true;
            StartCoroutine(LowHealthEffects());
        }
        else if (fill > 0.3f && isLowHealthEffect)
        {
            isLowHealthEffect = false;
            StopAllCoroutines();
            if (damageOverlay != null)
                damageOverlay.color = new Color(1, 0, 0, 0); // フェードアウト
            hpBarTransform.localScale = Vector3.one;
        }

        // テキスト更新
        if (hpText != null)
            hpText.text = $"HP: {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}";

    }

    private IEnumerator LowHealthEffects()
    {
        float pulse = 0f;
        float overlayAlpha = 0f;

        while (isLowHealthEffect && playerHealth.CurrentHealth > 0)
        {
            // HPバーのズーム（ドクンドクン）
            float scale = 1f + Mathf.PingPong(pulse * 4f, 0.2f);
            hpBarTransform.localScale = new Vector3(scale, scale, 1f);

            // 画面赤フェード（明滅）
            overlayAlpha = Mathf.PingPong(Time.time * 2f, 0.4f);
            if (damageOverlay != null)
                damageOverlay.color = new Color(1, 0, 0, overlayAlpha);

            pulse += Time.deltaTime;
            yield return null;
        }

        if (damageOverlay != null)
            damageOverlay.color = new Color(1, 0, 0, 0);
        hpBarTransform.localScale = Vector3.one;
    }
}
