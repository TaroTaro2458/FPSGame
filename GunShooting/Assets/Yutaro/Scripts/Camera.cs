using UnityEngine;

public class camera : MonoBehaviour
{
    //�}�E�X���x�ƃv���C���[�{�̂�Transform
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        //�J�[�\�����\�������b�N
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //�J�����̉�]
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

