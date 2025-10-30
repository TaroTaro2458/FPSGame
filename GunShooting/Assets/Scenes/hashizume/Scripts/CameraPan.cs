using UnityEngine;

public class MouseLookCamera : MonoBehaviour
{
    [Header("��]���x")]
    public float mouseSensitivity = 100f;

    [Header("��]����")]
    public float minY = -10f; // ������̍ő�p�x
    public float maxY = 30f;  // �������̍ő�p�x

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // �}�E�X�J�[�\�����\���ɂ��ČŒ�i�K�v�Ȃ�j
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // �㉺��]�i�����j
        xRotation = Mathf.Clamp(xRotation, minY, maxY); // �㉺����

        yRotation += mouseX; // ���E��]�i�����j

        // �J�����ɉ�]��K�p
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
