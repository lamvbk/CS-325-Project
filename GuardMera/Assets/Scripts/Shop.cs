using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject towerPrefab;
    public int cost = 50;
    public void PurchaseStandardTurret()
    {
        GameMaster.instance.SelectTower(towerPrefab, cost);
        Debug.Log("Standard tower purchased");
    }
}
