using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public TextMeshProUGUI healthCounterText;
    public TextMeshProUGUI moneyCounterText;

    public int health;
    public int money;

    public GameObject selectedTowerPrefab;

    public int selectedTowerCost;

    void Awake()
    {
        instance = this;
    }

    public bool CanBuild()
    {
        return selectedTowerPrefab != null && money >= selectedTowerCost;
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
    }

    public void SelectTower(GameObject prefab, int cost)
    {
        selectedTowerPrefab = prefab;
        selectedTowerCost = cost;
    }
    void Update()
    {
        healthCounterText.text = health.ToString();

        if(health <= 0)
        {
            healthCounterText.text = "0";
        }
        moneyCounterText.text = money.ToString();

        if(money <= 0)
        {
            moneyCounterText.text = "0";
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("YOU DIED");
        }
    }
}
