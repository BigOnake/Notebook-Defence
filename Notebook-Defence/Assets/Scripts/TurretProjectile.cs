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
    protected Projectile _currentProjectileLoaded;
    protected Transform projectileSpawnPosition;

    void Start()
    {
        _turret = GetComponent<Turret>();
        var projectilePrefab = _turret.GetCurrentProjectilePrefab();
        _pooler = new ObjectPooler<Projectile>(projectilePrefab, transform, 100, 200);
        projectileSpawnPosition = _turret.GetProjectileSpawnPosition();
        LoadProjectile();
    }

    protected virtual void Update()
    {
        if (_currentProjectileLoaded == null)
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime) //figure what this means
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null /*&& 
                _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f*/)
            {
                _currentProjectileLoaded.transform.SetParent(null);
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected virtual void LoadProjectile()
    {
        _currentProjectileLoaded = _pooler.GetObject();
        _currentProjectileLoaded.transform.localPosition = projectileSpawnPosition.position;
        _currentProjectileLoaded.transform.SetParent(projectileSpawnPosition);
        //_currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.InitializeProjectile(this, damage); //initializes values inside projectile for turret owner and damage
    }
}
