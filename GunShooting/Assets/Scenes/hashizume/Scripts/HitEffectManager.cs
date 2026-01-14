using UnityEngine;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager Instance;
    public GameObject defaultHitEffect;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnHitEffect(Vector3 pos, Vector3 normal)
    {
        Quaternion rot = Quaternion.LookRotation(normal);
        Instantiate(defaultHitEffect, pos + normal * 0.01f, rot);
    }
}
