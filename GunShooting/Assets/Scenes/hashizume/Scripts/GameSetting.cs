using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;

    public bool isSettingsOpen = false;

    const string SensXKey = "SensX";
    const string SensYKey = "SensY";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSensitivityX(float value)
    {
        mouseSensitivityX = value;
        PlayerPrefs.SetFloat(SensXKey, value);
    }

    public void SetSensitivityY(float value)
    {
        mouseSensitivityY = value;
        PlayerPrefs.SetFloat(SensYKey, value);
    }

    void LoadSettings()
    {
        mouseSensitivityX = PlayerPrefs.GetFloat(SensXKey, 1f);
        mouseSensitivityY = PlayerPrefs.GetFloat(SensYKey, 1f);
    }
}
