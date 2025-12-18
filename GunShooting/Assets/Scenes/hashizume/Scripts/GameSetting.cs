using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;
    public float bgmVolume = 1f;
    public float seVolume = 1f;
    public bool isSettingsOpen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSensitivityX(float value)
    {
        mouseSensitivityX = value;
    }

    public void SetSensitivityY(float value)
    {
        mouseSensitivityY = value;
    }

    public void SetBGM(float value)
    {
        bgmVolume = value;
        AudioListener.volume = value;
    }

    public void SetSE(float value)
    {
        seVolume = value;
    }
}
