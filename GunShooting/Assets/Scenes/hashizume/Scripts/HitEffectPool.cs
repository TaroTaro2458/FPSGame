using System.Collections.Generic;
using UnityEngine;

public class HitEffectPool : MonoBehaviour
{
    public static HitEffectPool Instance;
    public GameObject hitPrefab;
    public int poolSize = 30;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(hitPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void Spawn(Vector3 pos, Vector3 normal)
    {
        if (pool.Count == 0) return;

        GameObject obj = pool.Dequeue();
        obj.transform.position = pos + normal * 0.01f;
        obj.transform.rotation = Quaternion.LookRotation(normal);
        obj.SetActive(true);
        pool.Enqueue(obj);
    }
}
