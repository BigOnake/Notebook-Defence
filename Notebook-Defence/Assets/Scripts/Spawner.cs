using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private Enemy[] enemyPrefabs;

    [Header("Spawning")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delayBtwSpawns = 1f;

    private float _spawnTimer;
    private int _enemiesSpawned;

    private Dictionary<Enemy, ObjectPooler<Enemy>> _enemyPools;

    //Create the object pool
    private void Awake()
    {
        _enemyPools = new Dictionary<Enemy, ObjectPooler<Enemy>>();

        //Create pools dynamically based on the provided prefabs
        foreach (var enemyPrefab in enemyPrefabs)
        {
            _enemyPools[enemyPrefab] = new ObjectPooler<Enemy>(enemyPrefab, transform, enemyCount, enemyCount * 2);
        }
    }

    void Update()
    {
        if (_enemiesSpawned >= enemyCount) return; //Check if all enemies have been spawned and return if so

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0f)
        {
            _spawnTimer = delayBtwSpawns;
            SpawnEnemy();
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

}
