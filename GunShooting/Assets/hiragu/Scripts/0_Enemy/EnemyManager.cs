using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyInstance;

    private List<Transform> enemies = new List<Transform>();

    void Awake()
    {
        enemyInstance = this;
    }

    public void RegisterEnemy(Transform enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }

    public List<Transform> GetEnemies()
    {
        return enemies;
    }
}
