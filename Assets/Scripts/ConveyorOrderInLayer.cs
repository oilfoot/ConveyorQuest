using UnityEngine;

public class ConveyorOrderInLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Get all colliders currently overlapping with the sprite
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, spriteRenderer.bounds.size, 0);

        // Check each collider
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("cnvr1"))
            {
                spriteRenderer.sortingOrder = 8;
                return; // Exit the loop once the order is set
            }
            else if (collider.CompareTag("cnvr2"))
            {
                spriteRenderer.sortingOrder = 13;
                return; // Exit the loop once the order is set
            }
            else if (collider.CompareTag("cnvr3"))
            {
                spriteRenderer.sortingOrder = 18;
                return; // Exit the loop once the order is set
            }
        }

        // Reset the order in layer if not overlapping any conveyor trigger
        spriteRenderer.sortingOrder = 1; // Or whatever default order in layer you want
    }
}
