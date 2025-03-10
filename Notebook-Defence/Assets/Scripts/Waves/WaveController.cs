using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private int aliveEnemies;
    [SerializeField] private int currentWave;

    void Start()
    {
        Instance = this;

        enemySpawner.GetComponent<Spawner>();
    }

    void Update()
    {
        if(isAllDead())
        {
            //TODO Call to start a new wave
            /* 
             * enemySpawner.StartWave(currentWave);
             * SetCurrentEnemyCounter(enemySpawner.GetWaveEnemyCount());
             * IncreaseWave();
             */

        }
    }

    private void SetCurrentEnemyCounter(int spawnedEnemies)
    {
        if(spawnedEnemies > 0)
            aliveEnemies = spawnedEnemies;
    }

    public void DecreaseAliveEnemies()
    {
        if(aliveEnemies > 0)
        {
            aliveEnemies--;
        }
    }

    private bool isAllDead()
    {
        return (aliveEnemies <= 0);
    }

    private void IncreaseWaveCounter()
    {
        currentWave++;
    }
}
