using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Vector2 fixedDirection;

    [Header("Movment Settings")]
    public bool isHoming = true;
    public float speed = 10f;
    public float lifeSpan = 5f;

    [Header("Hit Settings")]
    public int damage = 50;
    public bool isSlowing;
    public bool isExplosive;
    public float explosionRadius = 2f;

    [Header("Melee Settings")]
    public bool isMelee;
    public Sprite chomp;
    private Enemy targetToHit;
    public Animator anim;
    [SerializeField]
    private float animTimer = 0.5f;

    public GameObject impactFX;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    public void Seek(Transform _target)
    {
        target = _target;

        if (!isHoming && target != null)
        {
                fixedDirection = transform.right;
                target = _target;
            }
            fixedDirection = transform.right;
            target = _target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoming)
        {
            if(target == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            if(!isMelee)
            {
                transform.Translate(fixedDirection *speed * Time.deltaTime, Space.World);
            }
            else
            {
                animTimer -= Time.deltaTime;
                if(animTimer <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);
    }
    void Damage(Transform enemy)
    {

        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {

            if (isSlowing)
            {
                EnemyMovement eSpeed = enemy.GetComponent<EnemyMovement>();
                eSpeed.ApplySlow(0.75f, 2f);
            }
            else
            {
                e.TakeDamage(damage);
            }
        }
    }

    public void Melee(Transform _target, Enemy _enemy)
    {
        // 1. Log that the function was reached
        Debug.Log($"[MELEE] Melee function called for: {gameObject.name}");

        if (anim != null) 
        {
            anim.SetTrigger("Chomp");
        }
        else 
        {
            Debug.LogWarning($"[MELEE] Animator is MISSING on {gameObject.name} in build!");
        }

        targetToHit = _enemy; 

        // 2. Immediate Check: Is the enemy reference valid?
        if (targetToHit != null)
        {
            Debug.Log($"[MELEE] Applying {damage} damage to {targetToHit.name}");
            targetToHit.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("[MELEE] targetToHit is NULL! Tower passed a dead or missing reference.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HitTargets(collision.transform);
        }
    }

    void HitTargets(Transform enemyHit)
    {
        if (impactFX != null)
        {
            Instantiate(impactFX, transform.position, transform.rotation);
        }
        if (isExplosive)
        {
            Explode(transform.position);
        }
        else
        {
            Damage(enemyHit);
        }
        Destroy(gameObject);
    }

    void Explode (Vector3 point)
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(point, explosionRadius);
        foreach (Collider2D col in objectsInRange)
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (isExplosive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }

}


