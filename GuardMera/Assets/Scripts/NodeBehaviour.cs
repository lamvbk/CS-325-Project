using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color orig_color;

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

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("turret placed");

        }
        
    }

    void OnMouseExit()
    {
        spriteRenderer.color = orig_color;
    }


}
