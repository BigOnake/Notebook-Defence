using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float initialHealth = 10f;
    //[SerializeField] private float maxHealth = 10f;
    [SerializeField]
    private ParticleSystem deathParticlesPrefab;

    public float CurrentHealth { get; private set; }

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        ResetHealth();
    }

    public void DealDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    public void ResetHealth() 
    { 
        CurrentHealth = initialHealth; 
    }

    protected virtual void Die()
    {
        if (deathParticlesPrefab != null)
        {
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        }
        _enemy.ResetEnemy();
    }
}
