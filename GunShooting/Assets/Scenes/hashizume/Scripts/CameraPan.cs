using UnityEngine;

public class MouseLookCamera : MonoBehaviour
{
    [Header("回転速度")]
    public float mouseSensitivity = 100f;

    [Header("回転制限")]
    public float minY = -10f; // 上方向の最大角度
    public float maxY = 30f;  // 下方向の最大角度

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // マウスカーソルを非表示にして固定（必要なら）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // 上下回転（垂直）
        xRotation = Mathf.Clamp(xRotation, minY, maxY); // 上下制限

        yRotation += mouseX; // 左右回転（水平）

        // カメラに回転を適用
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
