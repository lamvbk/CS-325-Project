using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 150;

    [HideInInspector]
    public float currentHealth;
    [SerializeField]
    private float currentHealthDecimal;
    //[SerializeField]
    //private bool damageTaken = false;
    [SerializeField]
    private SpriteRenderer sR;

    void Awake() // used to be Start()
    {
        sR = this.GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        //damageTaken = true;
        currentHealth -= amount;
        ChangeColor();

        if(currentHealth <= 0)  
        {
            Die();
        }
    }

    void ChangeColor()
    {
        currentHealthDecimal = currentHealth/maxHealth;
        Color damagedColor = new Color(1f, currentHealthDecimal, currentHealthDecimal);
        sR.color = damagedColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
