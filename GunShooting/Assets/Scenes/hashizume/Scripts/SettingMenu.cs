using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    bool isOpen = false;

    void Start()
    {
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    void ToggleSettings()
    {
        isOpen = !isOpen;
        settingsPanel.SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;

        Cursor.lockState = isOpen
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = isOpen;

        // Åö éãì_ëÄçÏí‚é~ÉtÉâÉO
        GameSetting.Instance.isSettingsOpen = isOpen;
    }
}
