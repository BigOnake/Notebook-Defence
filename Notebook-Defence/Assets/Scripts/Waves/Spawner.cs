using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int baseEnemyCount = 10;
    [SerializeField] private float linearGrowthFactor = 2f; // Enemies added per wave in early game
    [SerializeField] private float exponentialGrowthFactor = 1.15f; // Exponential scaling for later waves
    [SerializeField] private float randomVariation = 0.2f; // Random variation factor (e.g., ±20%)

    [SerializeField] float spawnRateIncrease = 1.0f;
    [SerializeField] float minSpawnDelay = 0.25f;
    [SerializeField] float maxSpawnDelay = 0.25f;
    [SerializeField] private Enemy[] enemyPrefabs;

    [Header("Spawning")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _baseDelayBtwSpawns = 1f;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemyCount;
    private float _delayBtwSpawns;

    private Dictionary<Enemy, ObjectPooler<Enemy>> _enemyPools;

    private WaveController _waveController;
    private bool waveActive = false;
    private int _waveNum = 1;

    //Create the object pool
    private void Awake()
    {
        _enemyPools = new Dictionary<Enemy, ObjectPooler<Enemy>>();

        _waveController = GetComponent<WaveController>();

        //Create pools dynamically based on the provided prefabs
        foreach (var enemyPrefab in enemyPrefabs)
        {
            _enemyPools[enemyPrefab] = new ObjectPooler<Enemy>(enemyPrefab, transform, _enemyCount, _enemyCount * 2);
        }
    }

    void Update()
    {
        if (!waveActive) return;

        if (_enemiesSpawned >= _enemyCount)
        {
            waveActive = false;
            return;
        }

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            _spawnTimer = _delayBtwSpawns;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;   

        Enemy randomEnemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)]; //change to only spawn certain enemies later

        if (_enemyPools.TryGetValue(randomEnemy, out var pool))
        {
            Enemy instance = pool.GetObject();
            instance.transform.position = spawnPoint.position;
            instance.Initialize(pool, spawnPoint);
            _enemiesSpawned++;
        }
    }

    public void StartWave(int waveNum)
    {
        waveActive = true;
        _waveNum = waveNum;

        // Scale enemy count with linear and exponential growth
        float expectedEnemyCount = baseEnemyCount + (waveNum * linearGrowthFactor) * Mathf.Pow(exponentialGrowthFactor, waveNum);

        float variationFactor = UnityEngine.Random.Range(1f - randomVariation, 1f + randomVariation);
        _enemyCount = Mathf.RoundToInt(expectedEnemyCount * variationFactor);
        _delayBtwSpawns = Mathf.Max(_baseDelayBtwSpawns * Mathf.Pow(spawnRateIncrease, waveNum - 1), minSpawnDelay);

        _enemiesSpawned = 0;
        _spawnTimer = _delayBtwSpawns;
    }

    public int GetWaveEnemyCount() { return _enemyCount; }
}
