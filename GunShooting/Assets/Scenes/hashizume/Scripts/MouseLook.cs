using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class MouseLook : MonoBehaviour
{
    float xRotation = 0f;

    void Update()
    {
        if (GameSetting.Instance.isSettingsOpen) return;

        float sensY = GameSetting.Instance.mouseSensitivityY;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
