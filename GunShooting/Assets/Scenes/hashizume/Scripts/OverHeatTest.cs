using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverHeatTest : MonoBehaviour
{
    [Header("Heat Settings")]
    [SerializeField] private float maxHeat = 100f;
    [SerializeField] private float cooldownRate = 20f;
    [SerializeField] private float cooldownDelay = 2f;

    [Header("UI")]
    [SerializeField] private Image heatFillImage;

    private float currentHeat = 0f;
    private float lastShotTime = 0f;
    private bool isOverheated = false;
    private bool isBlinking = false;  // 点滅中かどうか
    private Coroutine blinkCoroutine;

    public bool CanFire => !isOverheated;

    void Update()
    {
        // 冷却処理
        if (Time.time - lastShotTime > cooldownDelay && currentHeat > 0)
        {
            currentHeat -= cooldownRate * Time.deltaTime;
            currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

            // 点滅は冷却中も継続
            if (isOverheated && currentHeat <= 0f)
            {
                isOverheated = false;
            }
        }

        // 点滅開始/停止管理
        if (currentHeat >= maxHeat * 0.8f || isOverheated) // 80% 以上 or オーバーヒート
        {
            if (!isBlinking)
            {
                blinkCoroutine = StartCoroutine(BlinkFill());
                isBlinking = true;
            }
        }
        else
        {
            if (isBlinking)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                isBlinking = false;

                // 色を多段階グラデーションに戻す
                UpdateNormalColor();
            }
        }

        UpdateUI();
    }

    public void RegisterShot(float heatAmount)
    {
        currentHeat += heatAmount;
        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
        lastShotTime = Time.time;

        if (Mathf.Approximately(currentHeat, maxHeat))
        {
            isOverheated = true;
        }
    }

    private void UpdateUI()
    {
        if (heatFillImage == null) return;

        heatFillImage.fillAmount = currentHeat / maxHeat;

        if (!isBlinking)
        {
            UpdateNormalColor();
        }
    }

    // 通常色（多段階グラデーション）
    private void UpdateNormalColor()
    {
        float ratio = Mathf.Clamp01(currentHeat / maxHeat);

        if (ratio < 0.33f)
            heatFillImage.color = Color.Lerp(Color.green, Color.yellow, ratio / 0.33f);
        else if (ratio < 0.66f)
            heatFillImage.color = Color.Lerp(Color.yellow, Color.red, (ratio - 0.33f) / 0.33f);
        else
            heatFillImage.color = Color.Lerp(Color.red, new Color(0.5f, 0f, 0.5f), (ratio - 0.66f) / 0.34f);
    }

    // ゲージ部分のみ点滅
    private IEnumerator BlinkFill()
    {
        while (true)
        {
            heatFillImage.color = Color.cyan; // 点滅色
            yield return new WaitForSeconds(0.2f);
            UpdateNormalColor(); // 元の多段階色に戻す
            yield return new WaitForSeconds(0.2f);
        }
    }

    public float GetHeatRatio() => currentHeat / maxHeat;
}
