using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private int aliveEnemies;

    void Start()
    {
        Instance = this;
        enemySpawner.GetComponent<Spawner>();
    }

    void Update()
    {
        
    }

    private void SetCurrentEnemyCounter(int spawnedEnemies)
    {
        if(spawnedEnemies > 0)
            aliveEnemies = spawnedEnemies;
    }

    private void DecreaseAliveEnemies()
    {
        if(aliveEnemies > 0)
        {
            aliveEnemies--;
        }
    }
}
