using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody rb;
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
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 5f;

    private float dashTimer ;
    private bool isDashing = false;
    private float dashEndTime = 0f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 倒れ防止
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
        bool canDash = currentStamina > 0f;

        if (isTryingToDash && canDash)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
        }
        else
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    void FixedUpdate()
    {
        // 移動入力
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f) ? dashSpeed : moveSpeed;
        
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

    }
}

