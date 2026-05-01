using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Targeting Specific")]
    [SerializeField]
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    public Animator animator;

    //Targeting Specifics Maybe

    [Header("Bullet Tower(s) (default) Specs")]
    public float fireRate = 1f;
    public float fireCD = 0f;
    public GameObject projectilePf;

    [Header("Melee Specific Specs")]
    public bool isMelee;
    public GameObject meleePf;

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform firePoint;

    [Header("Upgrade Specific Specs")]

    public string tname = "";

    public int bulletCount = 1;
    public float spreadAngle = 0f;

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
                animator.SetBool("IsShoot", false);
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
            if(fireCD <= 0f)
            {
                Shoot();
                fireCD = 1f / fireRate;
            }

            fireCD -= Time.deltaTime;
        }
    }

    void LockOn()
    {
        Vector2 dir = target.position - transform.position;
        this.transform.right = dir;
    }

    void Shoot()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = (bulletCount > 1) ? (spreadAngle / (bulletCount - 1)) : 0;
        animator.SetBool("IsShoot", true);

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            GameObject projectileObject = (GameObject)Instantiate(projectilePf, firePoint.position, firePoint.rotation);
            projectileObject.transform.Rotate(0,0,currentAngle);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            
            if(projectile != null)
            {
                projectile.Seek(target);
            }
        }
    }
    void Shoot()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = (bulletCount > 1) ? (spreadAngle / (bulletCount - 1)) : 0;

        if(!isMelee)
        {    
            for (int i = 0; i < bulletCount; i++)
            {
                float currentAngle = startAngle + (i * angleStep);
                GameObject projectileObject = (GameObject)Instantiate(projectilePf, firePoint.position, firePoint.rotation);
                projectileObject.transform.Rotate(0,0,currentAngle);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                
                if(projectile != null)
                {
                    projectile.Seek(target);
                }
            }
        }
        else
        {
            GameObject meleeObject = (GameObject)Instantiate(meleePf, target.position, target.rotation);
            Projectile projectile = meleeObject.GetComponent<Projectile>();

            if(projectile != null)
            {
                Debug.Log("Calling melee script");
                projectile.Melee(target);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}