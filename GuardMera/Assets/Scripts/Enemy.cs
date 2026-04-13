using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void TakeDamage(float amount)
    {
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
