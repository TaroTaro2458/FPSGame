using UnityEngine;

public class camera : MonoBehaviour
{
    //マウス感度とプレイヤー本体のTransform
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        if (AbilitySelectionUI.isUIOpen) return;


        //カーソルを非表示かつロック
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //カメラの回転
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

