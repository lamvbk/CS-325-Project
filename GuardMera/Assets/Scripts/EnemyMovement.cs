using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    //private Enemy enemy;

    [SerializeField]
    private float changeDirThreshold = 0.4f;

    [Header("Test Values")]
    public float speed = 10f;

    void Start()
    {
        target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(Vector2.Distance(transform.position, target.position) <= changeDirThreshold)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            PathEnd();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void PathEnd()
    {
        Destroy(gameObject);
    }
}