using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private int curTargetPoint = 0;
    [SerializeField]
    private float speed = 0.1f;

    private EnemyWayPoints wayPoints;
    private ObjectPooler<Enemy> _enemyPool;
    public EnemyHealth enemyHealth;
    private Collider2D collider;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
        LoadWayPoints();
    }

    //initializes values after being pulled from pool by spawner
    public void Initialize(ObjectPooler<Enemy> pool, Transform spawnPoint)
    {
        _enemyPool = pool;
        spawnPosition = spawnPoint;
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        MoveToPoint(curTargetPoint);
        SetNewTargetPoint();
    }

    private void LoadWayPoints()
    {
        wayPoints = GameObject.FindGameObjectWithTag("WayPoints").GetComponent<EnemyWayPoints>();
    }

    private void MoveToPoint(int wayPointIndex)
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints.wayPointList[wayPointIndex].position, speed * Time.deltaTime);
    }

    private void SetNewTargetPoint()
    {
        float distance = Vector2.Distance(transform.position, wayPoints.wayPointList[curTargetPoint].position);

        if (distance < 0.1f)
        {
           if(curTargetPoint < wayPoints.wayPointList.Length - 1)
           { curTargetPoint++; }
        }
    }

    // Resets enemies to be pooled
    public void ResetEnemy()
    {
        curTargetPoint = 0;
        enemyHealth.ResetHealth();
        gameObject.SetActive(false); // Hide the enemy
        WaveController.Instance.DecreaseAliveEnemies();
        _enemyPool.ReleaseObject(this); // Return it to the pool
    }

    //public void ReleaseEnemy()
    //{
    //    _enemyPool.ReleaseObject();
    //}
}
