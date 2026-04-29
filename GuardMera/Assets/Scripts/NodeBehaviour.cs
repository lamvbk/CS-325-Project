using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color orig_color;

    private bool hasTower = false;

    public GameObject Tower;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        orig_color = spriteRenderer.material.color;
    }
    void OnMouseOver()
    {
        if (!hasTower && GameMaster.instance.CanBuild())
        {
            spriteRenderer.color = Color.red;
            if (!hasTower && Input.GetMouseButtonDown(0))
            {
                Instantiate(GameMaster.instance.selectedTowerPrefab, transform.position, Quaternion.Euler(0, 0, -90f));
                hasTower = true;
                Debug.Log("turret placed");
                GameMaster.instance.SpendMoney(GameMaster.instance.selectedTowerCost);
            }

        }
        else if (hasTower)
        {
            spriteRenderer.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                if (GameMaster.instance.selectedNode == this.gameObject)
                {
                    GameMaster.instance.ReturnToShop();
                    spriteRenderer.color = orig_color;

                }
                else
                {
                    GameMaster.instance.SelectNode(this.gameObject);
                    spriteRenderer.color = Color.yellow;
                }
            }
        }

        
    }

    void OnMouseExit()
    {
        spriteRenderer.color = orig_color;
    }


}
