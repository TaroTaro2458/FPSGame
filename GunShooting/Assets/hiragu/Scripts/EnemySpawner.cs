using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveSpawn spawner;

    void Start()
    {
        spawner.SpawnOne();  // 1‘Ì¶¬
        spawner.SpawnMultiple(5); // 5‘Ì¶¬
    }
}
