using UnityEngine;

public class GemFloat : MonoBehaviour
{
    public float rotateSpeed = 50f;
    public float floatHeight = 0.25f;
    public float floatSpeed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + Vector3.up * y;
    }
}
