using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // シェイクの持続時間と強さ
    [SerializeField] float shakeDuration = 0.3f;
    [SerializeField] float shakeMagnitude = 0.2f;

    // 初期位置の保存
    private Vector3 initialPosition;
    private float shakeTime = 0f;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeTime = shakeDuration;
    }
}
