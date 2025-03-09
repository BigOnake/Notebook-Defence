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

    private ObjectPool<Enemy> _enemyPool;

    void Start()
    {
        LoadWayPoints();
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

    public void SetSpawnPosition(Transform spawnPoint)
    {
        spawnPosition = spawnPoint;
        transform.position = spawnPoint.position;
    }
}
