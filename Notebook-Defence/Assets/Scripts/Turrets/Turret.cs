using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] private Projectile projectilePrefab;

    [Header("Tower Settings")]
    [SerializeField, UnityEngine.Range(0.0f, 25.0f)] private float fireRate = 1f;
    private float currFireRate = 1f;
    [SerializeField, UnityEngine.Range(0.25f, 25.0f)] private float attackRange = 4f;
    private float currAttackRange = 4f;

    [Header("Enemy Targets")]
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    public Enemy CurrentEnemyTarget;

    private CircleCollider2D _detectionCollider;
    private TurretProjectile _turretProjectile;

    void Start()
    {
        _detectionCollider = GetComponent<CircleCollider2D>();
        _detectionCollider.radius = attackRange;
        currAttackRange = attackRange;

        _turretProjectile = GetComponent<TurretProjectile>();
        _turretProjectile.ChangeFireDelay(fireRate);
        currFireRate = fireRate;
    }

    void Update()
    {
        GetCurrentEnemyTarget();
        if (currAttackRange != attackRange)
        {
            _detectionCollider.radius = attackRange;
            currAttackRange = attackRange;
        }
        if (currFireRate != fireRate)
        {
            _turretProjectile.ChangeFireDelay(fireRate);
            currFireRate = fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private void GetCurrentEnemyTarget()
    {
        for (int i = _enemies.Count - 1; i >= 0 ; i--) //loop through and remove all null enemies
        {
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
        }

        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
        }
        else
        {
            CurrentEnemyTarget = _enemies[0];
        }
    }

    public Transform GetProjectileSpawnPosition() { return projectileSpawnPosition; }
    public Projectile GetCurrentProjectilePrefab() { return projectilePrefab; }
}
