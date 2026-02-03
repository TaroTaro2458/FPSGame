using Unity.Services.Analytics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float heavyWeightMoveSpeed = 2.5f;

    [SerializeField] GetItem getItem;

    // Rigidbodyコンポーネントの参照
    private Rigidbody rb;
    // 地面チェック用のTransformとLayerMask
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float maxStamina = 100f;// 最大スタミナ
    private float currentStamina;  // 現在のスタミナ
    [SerializeField] float staminaDrainRate = 20f; // 毎秒減る量
    [SerializeField] float staminaRecoveryRate = 10f; // 毎秒回復する量

    //ジャンプの力
    [SerializeField] float jumpForce = 5f;
    // 地面に接地しているかどうか
    [SerializeField] bool isGrounded;
    // ダッシュの速度と持続時間
    [SerializeField] float dashMultiplier = 2f;
    [SerializeField] float dashDuration = 5f;
    private bool canDash = true;
    // アニメーター参照(保留中）
    //Animator animator;

    bool isFootstepPlaying;     // se用の動いているかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 倒れ防止
        currentStamina = maxStamina;
        //animator = GetComponent<Animator>();
    }

    void Update()
    {

        // 地面チェック
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

        // ジャンプ入力
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        bool isTryingToDash = Input.GetKey(KeyCode.LeftShift);

        // スタミナ処理）
        if (isTryingToDash && currentStamina > 0f)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            Debug.Log("ダッシュ中 " + currentStamina);
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }
        // Clampを最後に実行し、canDashも確定
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        canDash = currentStamina > 0f;  // ← ここで確定

    }

    void FixedUpdate()
    {
        // 移動入力
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        // アニメーションの速度パラメーターを設定
        float moveAmount = new Vector2(moveX, moveZ).magnitude;
        //animator.SetFloat("Speed", moveAmount);
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 重量に応じた基本速度を計算
        float baseSpeed = CalculateBaseSpeed();


        // ダッシュ中なら基礎速度に倍率をかける
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && canDash)
            ? baseSpeed * dashMultiplier
            : baseSpeed;


        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

        // 動いていたら足音がなるようにする
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (!isFootstepPlaying)
            {
                AudioManager.Instance.PlaySE3D(SEType.EnemyWalk, transform.position);
                isFootstepPlaying = true;
                Invoke(nameof(ResetFootstep), 10.0f); // 音の長さ
            }
        }
        else
        {
            isFootstepPlaying = false;
        }

    }

    // 重量に応じた基本速度を計算
    float CalculateBaseSpeed()
    {
        if (getItem == null) return moveSpeed;

        int currentWeight = getItem.CurrentInventoryWeight;
        int maxWeight = getItem.MaxWeight;

        // 重量超過時は重い速度
        if (currentWeight > maxWeight)
        {
            return heavyWeightMoveSpeed;
        }

        // 重量に応じて徐々に遅くする
        float weightRatio = (float)currentWeight / maxWeight;
        return Mathf.Lerp(moveSpeed, heavyWeightMoveSpeed, weightRatio);
        //return moveSpeed;
    }

    // se用
    void ResetFootstep()
    {
        isFootstepPlaying = false;
    }
}

