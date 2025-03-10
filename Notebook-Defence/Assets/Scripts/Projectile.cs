using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;

    private float _damage = 5f;
    private TurretProjectile _turretOwner;
    protected Enemy _enemyTarget;

    public float Damage 
    { 
        get => _damage; 
        set => _damage = value; 
    }
    public TurretProjectile TurretOwner
    {
        get => _turretOwner;
        set => _turretOwner = value;
    }

    protected virtual void Update()
    {
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget < minDistanceToDealDamage)
        {
            OnEnemyHit?.Invoke(_enemyTarget, _damage); //emitt that an enemy got hit
            _enemyTarget.enemyHealth.DealDamage(_damage);
            _enemyTarget = null;
            gameObject.SetActive(false);
            //_turretOwner.ResetTurretProjectile();
            _turretOwner.ReleaseProjectile(this);
        }
    }

    private void RotateProjectile()
    {
        if (_enemyTarget != null)
        {
            Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy; 
    }

    public void InitializeProjectile(TurretProjectile owner, float damage)
    {
        _turretOwner = owner;
        _damage = damage;
    }

    //public void ResetProjectile()
    //{
    //    _enemyTarget = null;
    //    transform.SetParent(null);
    //}
}
