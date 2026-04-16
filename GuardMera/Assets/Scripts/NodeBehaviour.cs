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
                Instantiate(GameMaster.instance.selectedTowerPrefab, transform.position, Quaternion.identity);
                hasTower = true;
                Debug.Log("turret placed");
                GameMaster.instance.SpendMoney(GameMaster.instance.selectedTowerCost);
            }

        }

        
    }

    void OnMouseExit()
    {
        spriteRenderer.color = orig_color;
    }


}
