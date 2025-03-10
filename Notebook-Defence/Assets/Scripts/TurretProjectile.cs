using UnityEngine;

//Script is used for controlling projectiles inside a turret/tower
public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }
    public float DelayPerShot {  get; set; }
    protected float _nextAttackTime;
    protected ObjectPooler<Projectile> _pooler;
    protected Turret _turret;
    protected Projectile _currentProjectileLoaded;
    
    void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler<Projectile>>();

        Damage = damage;
        DelayPerShot = delayBtwAttacks;
        LoadProjectile();
    }

    protected virtual void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }
        if (Time.time > _nextAttackTime) //figure what this means
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && 
                _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null; //explain this
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject instance = _pooler.GetObject();
        instance.transform.localPosition = projectileSpawnPosition.position;
        instance.transform.SetParent(projectileSpawnPosition);

        _currentProjectileLoaded = instance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.Damage = Damage;
        instance.SetActive(true);
    }

    bool IsTurretEmpty()
    {
        return (_turret == null);
    }
}
