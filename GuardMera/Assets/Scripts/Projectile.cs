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
            if(dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            transform.Translate(fixedDirection *speed * Time.deltaTime, Space.World);
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

        Damage(enemyHit);
        Destroy(gameObject);
    }

}


