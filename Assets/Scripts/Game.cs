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
        enemiesControl.SpawnEnemies(rows: 5, colums: 11, 
            viewportStartPosition: new Vector2(0.05f, 0.95f));
    }
}
