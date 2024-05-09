using UnityEngine;

public class SpriteOrderManager : MonoBehaviour
{
    public PlayerController playerController;
    public SpriteRenderer spriteRenderer;

    // Define order in layer for each line number
    public int Line1Layer = 1;
    public int Line2Layer = 2;
    public int Line3Layer = 3;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int currentLineNumber = playerController.currentLineNumber;
        
        if (currentLineNumber == 1)
        {
            spriteRenderer.sortingOrder = Line1Layer;
        } else if (currentLineNumber == 2)
        {
            spriteRenderer.sortingOrder = Line2Layer;
        } else if (currentLineNumber == 3)
        {
            spriteRenderer.sortingOrder = Line3Layer;
        }

    }
}
