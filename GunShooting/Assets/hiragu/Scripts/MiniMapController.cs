using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] RectTransform mapRect;          // マップパネル
    [SerializeField] RectTransform enemyDotPrefab;   // 敵アイコンプレハブ
    [SerializeField] Transform player;               // プレイヤー
    [SerializeField] float mapRange = 50f;           // ワールド単位での表示範囲
    [SerializeField] string enemyTag = "Enemy";      // 敵タグ
    [SerializeField] int poolSize = 32;

    private List<RectTransform> pool = new List<RectTransform>();
    private int usedCount = 0;

    void Awake()
    {
        InitPool(poolSize);
    }

    void LateUpdate()
    {
        if (player == null) return;

        usedCount = 0;
        Vector2 halfSize = mapRect.sizeDelta / 2f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemyObj in enemies)
        {
            Transform enemy = enemyObj.transform;

            // --- 1. プレイヤー中心に差分を取る ---
            Vector3 diff = enemy.position - player.position;

            // --- 2. プレイヤーの回転に合わせる ---
            float angleRad = player.eulerAngles.y * Mathf.Deg2Rad; // -で逆回転
            float x = diff.x * Mathf.Cos(angleRad) - diff.z * Mathf.Sin(angleRad);
            float y = diff.x * Mathf.Sin(angleRad) + diff.z * Mathf.Cos(angleRad);

            // --- 3. マップサイズに合わせてスケーリング ---
            Vector2 anchored = new Vector2(x, y) / mapRange * halfSize;

            // --- 4. マップ範囲内にクランプ ---
            float clampedX = Mathf.Clamp(anchored.x, -halfSize.x, halfSize.x);
            float clampedY = Mathf.Clamp(anchored.y, -halfSize.y, halfSize.y);
            Vector2 finalPos = new Vector2(clampedX, clampedY);

            // --- 5. アイコン配置 ---
            RectTransform dot = GetFromPool();
            dot.SetParent(mapRect, false);
            dot.anchoredPosition = finalPos;
        }

        // 使わなかったドットを非表示
        for (int i = usedCount; i < pool.Count; i++)
            pool[i].gameObject.SetActive(false);
    }

    void InitPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            RectTransform dot = Instantiate(enemyDotPrefab, mapRect.parent);
            dot.gameObject.SetActive(false);
            pool.Add(dot);
        }
    }

    RectTransform GetFromPool()
    {
        if (usedCount >= pool.Count)
        {
            RectTransform dot = Instantiate(enemyDotPrefab, mapRect.parent);
            pool.Add(dot);
        }
        RectTransform item = pool[usedCount];
        item.gameObject.SetActive(true);
        usedCount++;
        return item;
    }
}
