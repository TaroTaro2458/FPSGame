using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PlayerLook : MonoBehaviour
{
    void Update()
    {
        if (GameSetting.Instance.isSettingsOpen) return;

        float sensX = GameSetting.Instance.mouseSensitivityX;
        float mouseX = Input.GetAxis("Mouse X") * sensX;

        transform.Rotate(Vector3.up * mouseX);
    }
}
