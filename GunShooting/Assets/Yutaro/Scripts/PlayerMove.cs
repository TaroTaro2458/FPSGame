using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�ړ����x
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody rb;
    public Transform groundCheck;
    public LayerMask groundMask;

    //�W�����v�̗�
    [SerializeField] float jumpForce = 5f;

    [SerializeField] bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // �|��h�~
    }

    void Update()
    {
        // �n�ʃ`�F�b�N
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

        // �W�����v����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        //�O�㍶�E�̈ړ�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}

