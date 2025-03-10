using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerTurret : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Tower Settings")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float attackRange = 4f;

    [Header("Enemy Targets")]
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private Enemy CurrentEnemyTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fireCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentEnemyTarget();
        RotateProjectileTowardsTarget();
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
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateProjectileTowardsTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        projectileSpawnPos.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
