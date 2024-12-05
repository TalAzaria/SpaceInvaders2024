using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemiesControl: MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesPrefabs = new();
    [SerializeField] private int rows, columns, numberOfMoves;
    private Enemy[,] enemies;
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 moveDistance = new Vector2(0.4f, 0.8f);
    [SerializeField] private Transform topLeftSpawnPoint, bottomRightSpawnPoint;

    private void Awake()
    {
        enemies = new Enemy[rows, columns];
        OnScreenResolutionChanged(new Vector2(Screen.width, Screen.height));
    }

    private void Start()
    {
        ScreenManager.Instance.AddScreenBoundedObject(topLeftSpawnPoint);
        ScreenManager.Instance.AddScreenBoundedObject(bottomRightSpawnPoint);
        ScreenManager.Instance.OnScreenResolutionChanged += OnScreenResolutionChanged;
    }

    private void OnScreenResolutionChanged(Vector2 newResolution)
    {

    }

    public IEnumerator MoveEnemies()
    {

        while (true)
        {
            yield return MoveHorizontaly(Vector2.right * moveDistance.x, numberOfMoves);
            yield return MoveHorizontaly(Vector2.down * moveDistance.y, 1);
            yield return MoveHorizontaly(Vector2.left * moveDistance.x, numberOfMoves);
            yield return MoveHorizontaly(Vector2.down * moveDistance.y, 1);
        }
    }

    private IEnumerator MoveHorizontaly(Vector2 moveVector, int moveAmount)
    {
        for (int k = 0; k < numberOfMoves; k++)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    enemies[rows - i - 1, j].gameObject.transform.position += (Vector3)moveVector;
                    yield return new WaitForSeconds(1 / speed);
                }
            }
        }

        numberOfMoves = (int)((ScreenManager.Instance.ScreenWorldBounds.size.x -
            Mathf.Abs(bottomRightSpawnPoint.position.x + topLeftSpawnPoint.position.x -
            2 * (ScreenManager.Instance.ScreenWorldBounds.center.x - ScreenManager.Instance.ScreenWorldBounds.extents.x))) / moveDistance.x);
    }

    private void UpdateNumberOfHorizontalMove()
    {

    }

    public void SpawnEnemies()
    {
        int numOfRowsPerPrefab = (int)Mathf.Ceil((float)rows / (float)enemiesPrefabs.Count);
        int currentEnemyTypeIndex = 0, numOfRowsPerPrefabCounter = numOfRowsPerPrefab;
        Vector3 startSpawnPosition = topLeftSpawnPoint.position;
        Vector3 spawnPosition;
        Vector2 enemiesSpacingOffset = new Vector2(
            Mathf.Abs(topLeftSpawnPoint.position.x - bottomRightSpawnPoint.position.x) / (columns - 1),
            Mathf.Abs(topLeftSpawnPoint.position.y - bottomRightSpawnPoint.position.y) / (rows - 1));
        Enemy enemy = null;
        for (int i = 0; i < rows; i++)
        {
            spawnPosition = new Vector3(startSpawnPosition.x, startSpawnPosition.y - enemiesSpacingOffset.y * i, 0);
            for (int j = 0; j < columns; j++)
            {
                enemy = Instantiate(enemiesPrefabs[currentEnemyTypeIndex]);
                enemy.transform.position = spawnPosition;
                spawnPosition += new Vector3(enemiesSpacingOffset.x, 0, 0);
                ScreenManager.Instance.AddScreenBoundedObject(enemy.transform);
                this.enemies[i, j] = enemy;
            }

            if (i >= numOfRowsPerPrefabCounter - 1)
            {
                numOfRowsPerPrefabCounter += numOfRowsPerPrefab;
                currentEnemyTypeIndex++;
            }
        }
    }
}
