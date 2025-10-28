using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //移動速度
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody rb;
    public Transform groundCheck;
    public LayerMask groundMask;

    //ジャンプの力
    [SerializeField] float jumpForce = 5f;

    [SerializeField] bool isGrounded;


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
    }

    void FixedUpdate()
    {
        //前後左右の移動
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}

