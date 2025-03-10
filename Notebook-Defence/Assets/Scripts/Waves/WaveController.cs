using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private int aliveEnemies;
    [SerializeField] private int currentWave;
    [SerializeField] private int maxWave;

    void Start()
    {
        Instance = this;
        currentWave = 0;
        maxWave = 5;
        enemySpawner.GetComponent<Spawner>();
    }

    void Update()
    {
        if(isAllDead() && currentWave <= maxWave)
        {
            //TODO Call to start a new wave

            enemySpawner.StartWave(currentWave);
            SetCurrentEnemyCounter(enemySpawner.GetWaveEnemyCount());
            IncreaseWaveCounter();
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
