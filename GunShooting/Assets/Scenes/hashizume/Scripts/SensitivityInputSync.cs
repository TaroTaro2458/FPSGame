using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.HighDefinition;

public class SensitivityInputSync : MonoBehaviour
{
    public Slider sensXSlider;
    public TMP_InputField sensXInput;

    public Slider sensYSlider;
    public TMP_InputField sensYInput;

    float min = 0.1f;
    float max = 10f;

    // BGM 音量
    public Slider bgmSlider;
    public TMP_InputField bgmInput;
    // BGM 音量（0?1)
    float bgmMin = 0.0001f;
    float bgmMax = 1f;

    void Start()
    {
        // 初期同期
        SyncFromSettings();

        sensXSlider.onValueChanged.AddListener(OnXSliderChanged);
        sensYSlider.onValueChanged.AddListener(OnYSliderChanged);

        // ★ onValueChanged を使う（重要）
        sensXInput.onValueChanged.AddListener(OnXInputTyping);
        sensYInput.onValueChanged.AddListener(OnYInputTyping);

        sensXInput.onEndEdit.AddListener(OnXInputEnd);
        sensYInput.onEndEdit.AddListener(OnYInputEnd);

    }

    void SyncFromSettings()
    {
        sensXSlider.value = GameSetting.Instance.mouseSensitivityX;
        sensYSlider.value = GameSetting.Instance.mouseSensitivityY;

        sensXInput.text = sensXSlider.value.ToString("0.00");
        sensYInput.text = sensYSlider.value.ToString("0.00");
    }

    // ===== Slider =====

    void OnXSliderChanged(float value)
    {
        value = Mathf.Clamp(value, min, max);
        GameSetting.Instance.SetSensitivityX(value);
        sensXInput.text = value.ToString("0.00");
    }

    void OnYSliderChanged(float value)
    {
        value = Mathf.Clamp(value, min, max);
        GameSetting.Instance.SetSensitivityY(value);
        sensYInput.text = value.ToString("0.00");
    }

    // ===== Input typing =====

    void OnXInputTyping(string text)
    {
        ValidateInput(text, sensXSlider, GameSetting.Instance.SetSensitivityX);
    }

    void OnYInputTyping(string text)
    {
        ValidateInput(text, sensYSlider, GameSetting.Instance.SetSensitivityY);
    }

    // ===== Input end =====

    void OnXInputEnd(string text)
    {
        ForceCorrect(sensXSlider, sensXInput);
    }

    void OnYInputEnd(string text)
    {
        ForceCorrect(sensYSlider, sensYInput);
    }

    // ===== 共通処理 =====

    void ValidateInput(string text, Slider slider, System.Action<float> apply)
    {
        if (float.TryParse(text, out float value))
        {
            value = Mathf.Clamp(value, min, max);
            slider.SetValueWithoutNotify(value);
            apply(value);
        }
    }

    void ForceCorrect(Slider slider, TMP_InputField input)
    {
        float value = Mathf.Clamp(slider.value, min, max);
        input.text = value.ToString("0.00");
    }
}
