using UnityEngine;

public class HitLightAutoOff : MonoBehaviour
{
    void OnEnable()
    {
        Invoke(nameof(Disable), 0.2f);
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }

}
