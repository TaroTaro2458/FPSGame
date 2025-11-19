using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] Transform player;               // プレイヤー
    [SerializeField] GameObject enemyDotPrefab;      // 敵アイコンのプレハブ
    [SerializeField] RectTransform minimapArea;      // ミニマップの表示範囲（RectTransform）

    [Header("表示範囲や最大表示数の設定")]
    [SerializeField] float viewRadius = 20f;         // ミニマップに表示する範囲
    [SerializeField] int poolSize = 20;              // 敵ドットの最大数（ObjectPool）

    private List<RectTransform> dotPool = new List<RectTransform>();

    void Start()
    {
        // プレハブを poolSize 個だけ作る（これ以上増えない）
        for (int i = 0; i < poolSize; i++)
        {
            GameObject dot = Instantiate(enemyDotPrefab, minimapArea);
            dot.SetActive(false);
            dotPool.Add(dot.GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        UpdateEnemyDots();
    }

    void UpdateEnemyDots()
    {
        var enemies = EnemyManager.enemyInstance.GetEnemies();

        // プールを全て非表示にして準備
        foreach (var dot in dotPool)
            dot.gameObject.SetActive(false);

        int index = 0;

        foreach (Transform enemy in enemies)
        {
            if (index >= poolSize)
                break;

            Vector3 dir = enemy.position - player.position;
            float dist = dir.magnitude;

            if (dist > viewRadius)
                continue;

            // プレイヤーの向きに合わせる（Y軸のみ回転）
            Vector3 rotated = Quaternion.Euler(0, -player.eulerAngles.y, 0) * dir;

            // ミニマップ内の座標に変換
            Vector2 mapPos = new Vector2(rotated.x, rotated.z)
                             / viewRadius
                             * (minimapArea.sizeDelta / 2f);

            var dot = dotPool[index];
            dot.anchoredPosition = mapPos;
            dot.gameObject.SetActive(true);

            index++;
        }
    }
}
