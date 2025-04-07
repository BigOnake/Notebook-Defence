using UnityEngine;

//Script is used for controlling projectiles inside a turret/tower
public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected float delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    //public float Damage { get; set; }
    //public float DelayPerShot {  get; set; }
    protected float _nextAttackTime;
    protected ObjectPooler<Projectile> _pooler;
    protected Turret _turret;
    protected Transform projectileSpawnPosition;

    void Start()
    {
        _turret = GetComponent<Turret>();
        var projectilePrefab = _turret.GetCurrentProjectilePrefab();
        _pooler = new ObjectPooler<Projectile>(projectilePrefab, transform, 100, 200);
        projectileSpawnPosition = _turret.GetProjectileSpawnPosition();
    }

    protected virtual void Update()
    {
        if (Time.time >= _nextAttackTime && _turret.CurrentEnemyTarget != null) //redundant check? could just use an else
        {
            FireProjectile(_turret.CurrentEnemyTarget);
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected virtual void FireProjectile(Enemy target)
    {
        Projectile proj = _pooler.GetObject();
        proj.transform.localPosition = projectileSpawnPosition.position; //why local, gpt says just position
        proj.InitializeProjectile(this, damage);
        proj.SetEnemy(target);
        proj.RotateToEnemy();
    }

    //protected virtual void LoadProjectile() //is loading projectiles the best way to handle this? or should we load a projectile when it is fired, however could allow for a tower that builds up a stock
    //{
    //    _currentProjectileLoaded = _pooler.GetObject();
    //    _currentProjectileLoaded.transform.localPosition = projectileSpawnPosition.position;
    //    //_currentProjectileLoaded.ResetProjectile();
    //    _currentProjectileLoaded.InitializeProjectile(this, damage); //initializes values inside projectile for turret owner and damage
    //}

    public void ReleaseProjectile(Projectile projectile)
    {
        _pooler.ReleaseObject(projectile);
    }

    public void ChangeFireDelay(float fireRate)
    {
        delayBtwAttacks = 1f / fireRate;
    }
}