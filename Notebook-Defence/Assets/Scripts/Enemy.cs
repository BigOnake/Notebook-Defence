using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private int curTargetPoint = 0;
    [SerializeField]
    private float speed = 0.1f;

    private EnemyWayPoints wayPoints;

    void Start()
    {
        transform.position = spawnPosition.position;
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
}
