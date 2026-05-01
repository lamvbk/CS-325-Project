using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject snaketowerPrefab;
    public GameObject birdtowerPrefab;
    public GameObject liontowerPrefab;
    public int birdCost = 100;
     public int snakeCost = 100;
      public int lionCost = 100;
    public void PurchaseBirdTurret()
    {
        GameMaster.instance.SelectTower(birdtowerPrefab, birdCost);
    }

    public void PurchaseSnakeTurret()
    {
        GameMaster.instance.SelectTower(snaketowerPrefab, snakeCost);
    }

    public void PurchaseLionTurret()
    {
        GameMaster.instance.SelectTower(liontowerPrefab, lionCost);
    }
    
}
