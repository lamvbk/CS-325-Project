using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color orig_color;

    public bool hasTower = false;

    public GameObject itower;

    public float hoverTransparency = 0.4f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        orig_color = spriteRenderer.material.color;
        Color startColor = orig_color;
        startColor.a = 0f;
        spriteRenderer.color = startColor;
    }
    void OnMouseOver()
    {
        if (!hasTower && GameMaster.instance.CanBuild())
        {
            spriteRenderer.color = new Color(1f,0f,0f, hoverTransparency);
            if (!hasTower && Input.GetMouseButtonDown(0))
            {
                itower = Instantiate(GameMaster.instance.selectedTowerPrefab, transform.position, Quaternion.Euler(0,0,-90f));
                hasTower = true;
                GameMaster.instance.SpendMoney(GameMaster.instance.selectedTowerCost);
                spriteRenderer.color = new Color(0,0,0,0);
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
        Color exitColor = orig_color;
        exitColor.a = 0f;
        spriteRenderer.color = exitColor;
    }

    public void RemoveTower()
    {
        if(itower != null)
        {
            Destroy(itower);
        }
        hasTower = false;
        itower = null;
    }


}
