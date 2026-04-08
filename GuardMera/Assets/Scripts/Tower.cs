using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    //Targeting Specifics Maybe

    [Header("Bullet Tower(s) (default) Specs")]
    public float fireRate = 1f;
    public float fireCD = 0f;
    public GameObject bulletPf;

    [Header("Melee Specific Specs")]
    public bool isMelee;
    public GameObject meleePf;

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void UpdateTarget()
    {
        //Closest Enemy Targeting Scheme
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float sDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < sDistance)
            {
                sDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
            if (nearestEnemy != null && sDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
                targetEnemy = null;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            LockOn();
        }
    }

    void LockOn()
    {
        Vector2 dir = target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        this.transform.right = dir;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
