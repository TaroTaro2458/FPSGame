using UnityEngine;
using UnityEngine.AI;

public class WaveSpawn : MonoBehaviour
{
    [Header("スポーンエリア（このオブジェクト中心）")]
    [SerializeField] Vector2 spawnAreaSize = new Vector2(50f, 50f);

    [Header("プレイヤーからの最短距離")]
    [SerializeField] float minDistanceFromPlayer = 10f;

    [Header("NavMesh サンプル半径")]
    [SerializeField] float sampleRadius = 2f;

    [Header("複数敵プレハブ（配列長が1なら従来通り）")]
    [SerializeField] GameObject[] enemyPrefabs;

    [Tooltip("各プレハブの重み（確率）。0以上。未設定や長さ不一致なら均等配分になる）")]
    [SerializeField] float[] prefabWeights;

    [Header("検索設定")]
    [Tooltip("有効な候補点を探す最大試行回数")]
    [SerializeField] int maxAttempts = 30;

    [Header("参照")]
    [SerializeField] Transform player;

    // ---------- public API ----------
    // 1体だけスポーン
    public bool SpawnOne()
    {
        

        Vector3 spawnPos;
        if (!TryGetValidSpawnPoint(out spawnPos))
            return false;

        GameObject prefab = ChoosePrefab();
        if (prefab == null) return false;

        Instantiate(prefab, spawnPos, Quaternion.identity);
        return true;
    }

    // n体スポーン（成功した生成数を返す）
    public int SpawnMultiple(int count)
    {
        int spawned = 0;
        for (int i = 0; i < count; i++)
        {
            if (SpawnOne()) spawned++;
            else break; // 有効地点が見つからなければ中断
        }
        return spawned;
    }

    // ---------- 内部ロジック ----------
    bool TryGetValidSpawnPoint(out Vector3 result)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 random = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                0f,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            Vector3 candidate = transform.position + random;

            NavMeshHit hit;
            if (!NavMesh.SamplePosition(candidate, out hit, sampleRadius, NavMesh.AllAreas))
                continue;

            Vector3 navPos = hit.position;

            if (player != null)
            {
                float dist = Vector3.Distance(player.position, navPos);
                if (dist < minDistanceFromPlayer)
                    continue;
            }

            result = navPos;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    GameObject ChoosePrefab()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return null;
        if (enemyPrefabs.Length == 1) return enemyPrefabs[0];

        // 重みが有効かチェック
        bool hasValidWeights = prefabWeights != null && prefabWeights.Length == enemyPrefabs.Length;
        if (!hasValidWeights)
        {
            // 均等に選ぶ
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }

        // 重み合計
        float total = 0f;
        for (int i = 0; i < prefabWeights.Length; i++)
            total += Mathf.Max(0f, prefabWeights[i]);

        if (total <= 0f)
        {
            // 全部0なら均等
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }

        // 重み付き選択
        float r = Random.value * total;
        float acc = 0f;
        for (int i = 0; i < prefabWeights.Length; i++)
        {
            acc += Mathf.Max(0f, prefabWeights[i]);
            if (r <= acc)
                return enemyPrefabs[i];
        }

        // 保険
        return enemyPrefabs[enemyPrefabs.Length - 1];
    }

    // エディタ可視化
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0.2f, spawnAreaSize.y));
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, minDistanceFromPlayer);
        }
    }
}
