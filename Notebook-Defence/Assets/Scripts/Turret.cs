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
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float attackRange = 4f;

    [Header("Enemy Targets")]
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    public Enemy CurrentEnemyTarget;

    void Start()
    {
        //fireCooldown = 0f;
    }

    void Update()
    {
        GetCurrentEnemyTarget();
       // RotateProjectileTowardsTarget();
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

    //private void RotateProjectileTowardsTarget()
    //{
    //    if (_enemies.Count <= 0)
    //    {
    //        CurrentEnemyTarget = null;
    //        return;
    //    }

    //    Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
    //    float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
    //    projectileSpawnPosition.rotation = Quaternion.Euler(0f, 0f, angle);
    //}

    public Transform GetProjectileSpawnPosition() { return projectileSpawnPosition; }
    public Projectile GetCurrentProjectilePrefab() { return projectilePrefab; }
}
