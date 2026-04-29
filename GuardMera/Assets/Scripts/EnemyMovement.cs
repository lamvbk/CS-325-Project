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

    public float baseSpeed = 10f;
    public float slowTimer = 0f;

    public int damage = 50;

    void Start()
    {
        target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if(slowTimer <= 0)
            {
                speed = baseSpeed;
                Debug.Log("Speed is back up to normal!");
            }
        }

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
        GameMaster.instance.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void ApplySlow(float slowPercent, float duration)
    {
        speed = baseSpeed * (1f - slowPercent);
        slowTimer = duration;
    }
}