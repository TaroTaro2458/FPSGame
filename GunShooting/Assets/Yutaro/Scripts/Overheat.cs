using UnityEngine;

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

    void Update()
    {
        // 冷却処理
        if (Time.time - lastShotTime > cooldownDelay && currentHeat > 0)
        {
            currentHeat -= cooldownRate * Time.deltaTime;
            currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

            if (isOverheated && currentHeat <= 0f)
            {
                isOverheated = false;
                Debug.Log("冷却完了");
            }
        }
    }

    public void RegisterShot(float heatPerShot)
    {
        // 射撃時のゲージ上昇処理
        if (isOverheated) return;

        currentHeat += heatPerShot;
        lastShotTime = Time.time;

        // オーバーヒート判定
        if (currentHeat >= maxHeat)
        {
            isOverheated = true;
            Debug.Log("オーバーヒート！");
        }
    }
    // ゲージの割合を取得
    public float GetHeatRatio()
    {
        return currentHeat / maxHeat;
    }
}

