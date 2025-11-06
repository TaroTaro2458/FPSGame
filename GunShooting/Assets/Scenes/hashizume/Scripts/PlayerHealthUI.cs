using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHelth playerHealth; // プレイヤーの参照
    [SerializeField] private Image hpFillImage;        // HPバー本体（Filled）
    [SerializeField] private TextMeshProUGUI hpText;   // HPテキスト
    [SerializeField] private RectTransform hpBarTransform; // HPバー全体のRectTransform

    private float previousFillAmount;
    private bool isAnimating = false;

    private void Start()
    {
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<PlayerHelth>();

        if (hpBarTransform == null && hpFillImage != null)
            hpBarTransform = hpFillImage.GetComponent<RectTransform>();

        previousFillAmount = 1f;
    }

    private void Update()
    {
        if (playerHealth == null || hpFillImage == null) return;

        float fillAmount = Mathf.Clamp01((float)playerHealth.CurrentHealth / playerHealth.MaxHealth);

        // HPバー更新
        hpFillImage.fillAmount = fillAmount;
        hpFillImage.color = Color.Lerp(Color.red, Color.green, fillAmount);

        // テキスト更新
        if (hpText != null)
            hpText.text = $"HP: {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}";

        // HPが減ったときに演出を開始
        if (fillAmount < previousFillAmount && !isAnimating)
            StartCoroutine(ShakeBar());

        previousFillAmount = fillAmount;
    }

    /// <summary>
    /// HPバーを一瞬揺らす
    /// </summary>
    private IEnumerator ShakeBar()
    {
        isAnimating = true;

        Vector3 originalPos = hpBarTransform.localPosition;
        float duration = 0.25f; // 揺れる時間
        float strength = 10f;   // 揺れの強さ

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float offsetX = Random.Range(-strength, strength);
            float offsetY = Random.Range(-strength, strength);
            hpBarTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        hpBarTransform.localPosition = originalPos;
        isAnimating = false;
    }
}
