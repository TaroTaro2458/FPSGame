using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Overheat : MonoBehaviour
{
    // 最大射撃可能ゲージ
    [SerializeField] float maxHeat = 100f;
    //ゲージ拡張用
    [SerializeField] float baseMaxHeat = 100f;
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
    // Slider本体のImage
    [SerializeField] Image sliderImage; 
    // スライダーの塗りつぶし
    private Image fillImage;
    // 点滅用
    private Coroutine blinkCoroutine;

    // 冷却アタッチメントによる冷却速度追加分
    [SerializeField] float baseCooldownRate = 20f;
    private float currentCooldownRate;
    // 冷却アタッチメント参照
    [SerializeField] CoolingAttachment coolingAttachment;
    // 冷却アタッチメントのリスト
    [SerializeField] List<CoolingAttachment> coolingAttachments = new List<CoolingAttachment>();
    // ゲージ拡張用アタッチメント
    [SerializeField] MaxHeatAttachment  maxHeatgageAttachment;
    [SerializeField] List<MaxHeatAttachment> maxHeatgageAttachments = new List<MaxHeatAttachment>();
    public float additionalMaxHeat;
    
    void Start()
    {
        RecalculateMaxHeat();
        RecalculateCooldownRate();

        currentHeat = 0f;

        // スライダーの最大値設定
        if (heatSlider != null)
        {
            heatSlider.maxValue = maxHeat;
            heatSlider.value = currentHeat;
            fillImage = heatSlider.fillRect.GetComponent<Image>();
        }

        // アタッチメントがある場合、パラメータを上書き
        if (coolingAttachment != null)
        {
            currentCooldownRate += coolingAttachment.additionalCooldownRate;
        }
        else if (maxHeatgageAttachment != null)
        {
            maxHeat += maxHeatgageAttachment.additionalMaxHeat;
            heatSlider.maxValue = maxHeat;
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
            currentHeat -= currentCooldownRate * Time.deltaTime;
            currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

            if (isOverheated && currentHeat <= 0f)
            {
                isOverheated = false;
                //Debug.Log("冷却完了");

                // 点滅停止
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    fillImage.color = Color.white; // 元の色に戻す
                }

                AudioManager.Instance.PlaySE3D(SEType.Reload, transform.position);　// 冷却完了したらse
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
            //Debug.Log("オーバーヒート！");

            AudioManager.Instance.PlaySE3D(SEType.OverHeat, transform.position); // オーバーヒートしたらseをならす

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
            Color c = sliderImage.color;
            c.a = 0f;
            sliderImage.color = c;
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

    //ゲッター更新
    public float GetCooldownRate() {
        return currentCooldownRate; 
    }
    public void SetAttachment(CoolingAttachment attachment) 
    {
        coolingAttachment = attachment; 
        currentCooldownRate = baseCooldownRate + (attachment != null ? attachment.additionalCooldownRate : 0f);
    }

    // 冷却速度を再計算
    public void RecalculateCooldownRate() 
    { 
        currentCooldownRate = baseCooldownRate; 
        foreach (var attachment in coolingAttachments) 
        { 
            if (attachment != null) 
            { 
                currentCooldownRate += attachment.additionalCooldownRate;
            } 
        } 
    }

    // 最大熱量を再計算
    public void RecalculateMaxHeat()
    {
        maxHeat = baseMaxHeat;
        foreach (var attachment in maxHeatgageAttachments)
        {
            if (attachment != null)
            {
                maxHeat += attachment.additionalMaxHeat;
            }
        }

        if (heatSlider != null)
        {
            heatSlider.maxValue = maxHeat;
        }
    }


    // アタッチメントを追加
    public void AddCooldownAttachment(CoolingAttachment newAttachment) 
    {
        if (newAttachment != null) 
        {
            coolingAttachments.Add(newAttachment); 
            RecalculateCooldownRate();
            //Debug.Log($"アタッチメント追加！冷却速度: {currentCooldownRate}");
        } 
    }

    public void AddMaxHeatAttachment(MaxHeatAttachment newAttachment)
    {
        if (newAttachment != null)
        {
            maxHeatgageAttachments.Add(newAttachment);
            RecalculateMaxHeat();
            //Debug.Log($"アタッチメント追加！, 最大ヒート: {maxHeat}");
        }
    }
}

