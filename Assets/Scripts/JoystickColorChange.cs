using UnityEngine;
using UnityEngine.UI;

public class JoystickColorChange : MonoBehaviour
{
    public Button playButton; // Assign the Play button in the inspector
    private Color originalColor;
    private Color targetColor = Color.yellow;
    private bool isYellow = false;
    private bool joystickWasMoved = false;

    private void Start()
    {
        // Save the original color of the button
        originalColor = playButton.image.color;
    }

    void Update()
    {
        // Check if the joystick is moved
        if (IsJoystickMoved())
        {
            // Toggle the color only if the joystick was not previously moved
            if (!joystickWasMoved)
            {
                if (isYellow)
                {
                    playButton.image.color = originalColor;
                }
                else
                {
                    playButton.image.color = targetColor;
                }

                // Toggle the boolean
                isYellow = !isYellow;
                joystickWasMoved = true;
            }
        }
        else
        {
            // Reset the joystick move state when the joystick is not being moved
            joystickWasMoved = false;
        }
    }

    bool IsJoystickMoved()
    {
        // Check if any joystick axis is moved
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            return true;
        }
        return false;
    }
}
