using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemiesControl : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesPrefabs = new();
    private Dictionary<Enemy, GameObject> enemies = new();
    [SerializeField] private float speed = 3;

    public void SpawnEnemies(int rows, int colums, Vector2 viewportStartPosition)
    {
        Vector3 startSpawnPosition = Camera.main.ViewportToWorldPoint(viewportStartPosition);
        Vector3 spawnPosition = new Vector3(startSpawnPosition.x, 0, 0);
        int numOfRowsPerPrefab = enemiesPrefabs.Count;
        Enemy enemy = null;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                enemy = Instantiate(enemiesPrefabs[i % numOfRowsPerPrefab]);
                enemy.transform.position = spawnPosition;
                spawnPosition += new Vector3(enemy.SpriteBounds.size.x * 2, 0, 0);
                ScreenManager.Instance.AddScreenBoundedObject(enemy.transform);
                enemies.Add(enemy, enemy.gameObject);
            }

            spawnPosition = new Vector3(startSpawnPosition.x, enemy.SpriteBounds.size.y * i, 0);
        }
    }
}
