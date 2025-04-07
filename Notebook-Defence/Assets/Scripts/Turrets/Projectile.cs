using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float moveSpeed = 10f;

    private float _damage = 5f;
    private TurretProjectile _turretOwner;
    protected Enemy _enemyTarget;
    private Collider2D _collider;

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

    protected virtual void Update() // modify this so that it simply moves projectile or checks components and then handles updates
    {
        MoveProjectile();
    }

    protected virtual void MoveProjectile()
    {
        Vector2 baseMovement = transform.up * moveSpeed * Time.deltaTime; //adds basic forward movement
        Vector2 additionalMovement = Vector2.zero;

        foreach (var mover in GetComponents<IProjectileMover>()) //loops through all projectile mover components and adds their movement modifier
        {
            additionalMovement += mover.ModifyMovement(this);
        }

        Vector2 totalMovement = baseMovement + additionalMovement;
        transform.position += new Vector3(totalMovement.x, totalMovement.y, 0f);
    }

    //HOMING
    //transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed* Time.deltaTime); //replace with generic move forward, but save code for homing component
    //float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude; //should be removed as we do not care about the distance to target
    //if (distanceToTarget<minDistanceToDealDamage) //also should be removed as we will be using collisions
    //{
    //    OnEnemyHit?.Invoke(_enemyTarget, _damage); //emitt that an enemy got hit //also figure what this is doing or triggering
    //    _enemyTarget.enemyHealth.DealDamage(_damage);
    //    _enemyTarget = null;
    //    gameObject.SetActive(false);
    //    //_turretOwner.ResetTurretProjectile(); //determine if necessary
    //    _turretOwner.ReleaseProjectile(this);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnEnemyHit?.Invoke(enemy, _damage);
            enemy.enemyHealth.DealDamage(_damage);
            gameObject.SetActive(false);
            _turretOwner?.ReleaseProjectile(this);
        }
    }

    public void RotateToEnemy() //may want to look back at this for future component implementation (different types of projectile movement e.g., boomerang, gatling gun lock on
    {
        if (_enemyTarget != null)
        {
            Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
    }

    public void InitializeProjectile(TurretProjectile owner, float damage) //necessary? what is use of get set
    {
        _turretOwner = owner;
        _damage = damage;
        _collider = GetComponent<Collider2D>();
    }

    public void SetEnemy(Enemy enemy) //could allow for switching targets mid-fire
    {
        _enemyTarget = enemy; 
    }

    //public void ResetProjectile()
    //{
    //    _enemyTarget = null;
    //    transform.SetParent(null);
    //}
}
