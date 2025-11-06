using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overheat : MonoBehaviour
{
    // 最大射撃可能ゲージ
    [SerializeField] float maxHeat = 100f;
    // 1発あたりのゲージ上昇量
    [SerializeField] float heatPerShot = 10f;
    // 冷却速度（ゲージ/秒）
    [SerializeField] float cooldownRate = 20f;
    // 冷却開始までの遅延時間（秒）
    [SerializeField] float cooldownDelay = 2f;

    // 現在のゲージ量
    private float currentHeat = 0f;
    // 最後に射撃した時間
    private float lastShotTime = 0f;
    // オーバーヒート状態か
    private bool isOverheated = false;

    public bool CanFire => !isOverheated;

    //  UIスライダー参照
    [SerializeField] Slider heatSlider;
    // スライダーの塗りつぶし
    private Image fillImage;
    // 点滅用
    private Coroutine blinkCoroutine;


    void Start()
    {
        // スライダーの最大値設定
        if (heatSlider != null)
        {
            heatSlider.maxValue = maxHeat;
            heatSlider.value = currentHeat;
            fillImage = heatSlider.fillRect.GetComponent<Image>();
        }

        
    }
    void Update()
    {
        if (heatSlider != null)
        {
            heatSlider.value = currentHeat;
        }

        // 冷却処理
        if (Time.time - lastShotTime > cooldownDelay && currentHeat > 0)
        {
            currentHeat -= cooldownRate * Time.deltaTime;
            currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

            if (isOverheated && currentHeat <= 0f)
            {
                isOverheated = false;
                Debug.Log("冷却完了");

                // 点滅停止
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    fillImage.color = Color.white; // 元の色に戻す
                }
            }


        }

        //  UI更新
        if (heatSlider != null)
        {
            heatSlider.value = currentHeat;
        }
    }

    public void RegisterShot(float heatPerShot)
    {
        // 射撃時のゲージ上昇処理
        if (isOverheated) return;

        currentHeat += heatPerShot;
        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
        lastShotTime = Time.time;

        // オーバーヒート判定
        if ((Mathf.Approximately(currentHeat, maxHeat)))
        {
            isOverheated = true;
            Debug.Log("オーバーヒート！");

            // 点滅開始
            if (blinkCoroutine == null && fillImage != null)
            {
                blinkCoroutine = StartCoroutine(BlinkFill());
            }

        }



    }

    // 塗りつぶしを点滅させる
    IEnumerator BlinkFill()
    {
        while (true)
        {
            //赤と白を交互に切り替え
            fillImage.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            fillImage.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
    }
    // ゲージの割合を取得
    public float GetHeatRatio()
    {
        return currentHeat / maxHeat;
    }
}

