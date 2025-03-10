using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] float spawnRateIncrease = 1.0f;
    [SerializeField] float minSpawnDelay = 0.25f;
    [SerializeField] private Enemy[] enemyPrefabs;

    [Header("Spawning")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delayBtwSpawns = 1f;

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
            _enemyPools[enemyPrefab] = new ObjectPooler<Enemy>(enemyPrefab, transform, enemyCount, enemyCount * 2);
        }
    }

    void Update()
    {
        if (waveActive)
        {
            if (_enemiesSpawned >= enemyCount)
            {
                waveActive = false;
                return;
            }

            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0f)
            {
                _spawnTimer = delayBtwSpawns;
                SpawnEnemy();
            }
        }
        
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;   

        Enemy randomEnemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];

        if (_enemyPools.TryGetValue(randomEnemy, out var pool))
        {
            Enemy instance = pool.GetObject();
            instance.transform.position = spawnPoint.position;
            instance.Initialize(pool, spawnPoint);
            _enemiesSpawned++;
        }
    }

    public void startWave(int waveNum)
    {
        waveActive = true;
        _waveNum = waveNum;
    }


}
