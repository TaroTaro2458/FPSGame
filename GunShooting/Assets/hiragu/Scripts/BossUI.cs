using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void UpdateBossUI(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
