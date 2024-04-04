using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public float stoppingDistance = 0.1f; // Distance at which the player stops moving towards the target
    public GameObject targetObject; // The GameObject the player should move towards

    // Jump settings
    public float jumpDuration = 0.5f; // Duration of the jump (time the collider will be deactivated)
    public float jumpCooldown = 1f; // Cooldown between jumps
    private float jumpTimer = 0f; // Timer to track the cooldown
    private bool isJumping = false; // Flag to check if jump is currently active

    void Update()
    {
        if (targetObject != null)
        {
            MoveTowardsTarget();
            HandleJump();
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 targetPosition = targetObject.transform.position;
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, targetPosition);

        if (distance > stoppingDistance && !isJumping)
        {
            // Move towards the target position
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        // Cooldown management
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimer <= 0 && !isJumping)
        {
            Jump();
        }

        // Deactivate collider if jump is active
        if (isJumping)
        {
            jumpDuration -= Time.deltaTime;
            if (jumpDuration <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                isJumping = false;
                jumpTimer = jumpCooldown;
                jumpDuration = 0.5f; // Reset jump duration for next jump
            }
        }
    }

    void Jump()
    {
        // Deactivate collider and set jumping flag
        GetComponent<BoxCollider2D>().enabled = false;
        isJumping = true;
    }
}
