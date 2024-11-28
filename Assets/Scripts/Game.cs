using UnityEngine;

public class Game : MonoBehaviour
{
    private EnemiesControl enemiesControl;
    
    private void Awake()
    {
        enemiesControl = GetComponent<EnemiesControl>();
    }

    private void Start()
    {
        enemiesControl.SpawnEnemies();
        StartCoroutine(enemiesControl.MoveEnemies());
    }
}
