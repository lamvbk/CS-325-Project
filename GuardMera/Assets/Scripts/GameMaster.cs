using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public TextMeshProUGUI healthCounterText;
    public TextMeshProUGUI moneyCounterText;

    public int health;
    public int money;

//THIS IS FOR THE SHOP MENU AND TOWER PLACEMENTS
    public GameObject selectedTowerPrefab;
    public int selectedTowerCost;
     public GameObject shopMenu;

//THIS IS FOR FUSING AND TOWER SELECTION FOR THE FUSE MENU
    public GameObject fuseMenu;
    public GameObject selectedNode;

    public FuseMenu fuseMenuScript;

    public GameObject hydraPrefab;
    

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
            death();
        }
    }

    public void SelectNode( GameObject node)
    {
        selectedTowerPrefab = null;
        selectedTowerCost = 0;
        selectedNode = node;
        NodeBehaviour nb = node.GetComponent<NodeBehaviour>();
        string tName = nb.itower.GetComponent<Tower>().tname;
        shopMenu.SetActive(false);
        fuseMenu.SetActive(true);
        fuseMenuScript.SetupMenu(tName);
    }

    public void ReturnToShop()
    {
        selectedNode = null;
        fuseMenu.SetActive(false);
        shopMenu.SetActive(true);
    }



    public void death()
    {
        Debug.Log("YOU DIED");
    }

    public void CombineTower(string baseName, string addition)
    {
        string recipe = baseName + "_" + addition;
        GameObject finalPrefab = null;

        if (recipe == "Snake_Snake") finalPrefab = hydraPrefab;

        if (finalPrefab != null)
        {
            NodeBehaviour nb = selectedNode.GetComponent<NodeBehaviour>();
            nb.RemoveTower(); 
            
            // Spawn New
            GameObject newTower = Instantiate(finalPrefab, selectedNode.transform.position, Quaternion.Euler(0, 0, -90f));
            nb.itower = newTower;
            nb.hasTower = true;

            ReturnToShop();
        }
        else
        {
            Debug.Log("Invalid Recipe!");
        }
    }
}
