using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivityValueUI : MonoBehaviour
{
    [Header("X Sensitivity")]
    public Slider sensXSlider;
    public TextMeshProUGUI sensXText;

    [Header("Y Sensitivity")]
    public Slider sensYSlider;
    public TextMeshProUGUI sensYText;

    void Start()
    {
        // 初期表示
        UpdateX(sensXSlider.value);
        UpdateY(sensYSlider.value);

        // スライダー変更時に更新
        sensXSlider.onValueChanged.AddListener(UpdateX);
        sensYSlider.onValueChanged.AddListener(UpdateY);
    }

    void UpdateX(float value)
    {
        sensXText.text = value.ToString("0.00");
    }

    void UpdateY(float value)
    {
        sensYText.text = value.ToString("0.00");
    }
}
