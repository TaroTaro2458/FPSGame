using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemySpawn spawner;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] int spawnNum = 3;
    [SerializeField] float bossSpawnInterval = 60f;

    [Header("waveの設定")]
    [SerializeField] float[] waveCount;

    float time = 0;
    float count = 0;
    float bossCnt = 0;
    int waveIndex = 0;
    
    void Start()
    {
        spawner.SpawnMultiple(spawnNum);
    }

    private void Update()
    {
        // 時間経過
        time += Time.deltaTime;
        count += Time.deltaTime;
        bossCnt += Time.deltaTime;

        // 雑魚敵を生成
        if (count >= spawnInterval)
        {
            spawner.SpawnMultiple(spawnNum);
            count = 0;
        }

        // ボスを生成
        if(bossCnt >= bossSpawnInterval)
        {
            spawner.BossSpawn();
            bossCnt = 0;
        }

        //　waveがあれば進める
        if (waveIndex < waveCount.Length && time >= waveCount[waveIndex])
        {
            spawnInterval -= 1f;
            spawnNum += 1;
            waveIndex += 1;
        }
    }
}
