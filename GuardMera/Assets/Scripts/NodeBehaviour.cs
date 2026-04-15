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
        if (spriteRenderer.color != Color.red)
        {
            spriteRenderer.color = Color.red;

        }

        if (!hasTower && Input.GetMouseButtonDown(0))
        {
            Instantiate(Tower, transform.position, Quaternion.identity);
            hasTower = true;
            Debug.Log("turret placed");

        }
        
    }

    void OnMouseExit()
    {
        spriteRenderer.color = orig_color;
    }


}
