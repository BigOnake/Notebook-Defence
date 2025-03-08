using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private Enemy enemyPrefab;

    [Header("Spawning")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delayBtwSpawns = 1f;


    private float _spawnTimer;
    private int _enemiesSpawned;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            CreateEnemy,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPooledObject,
            true,
            500,
            1000
            );

    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            _spawnTimer = delayBtwSpawns;
            if (_enemiesSpawned < enemyCount)
            {
                SpawnEnemy();
            }
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy enemyInstance = Instantiate(enemyPrefab);
        enemyInstance.SetPool(_enemyPool);
        return enemyInstance;
    }

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.SetSpawnPosition(spawnPoint);
        enemy.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemy()
    {
        Enemy newInstance = _enemyPool.Get();
        _enemiesSpawned++;
    }

}
