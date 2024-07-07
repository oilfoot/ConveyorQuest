using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RetroNameInput : MonoBehaviour
{
    public TMP_Text[] letters; // Assign in Inspector
    public Button saveScoreButton; // Assign in Inspector
    public Button restartButton; // Assign in Inspector
    public GameObject requiredGameObject; // The GameObject that needs to be active

    private int currentLetterIndex = 0;
    private char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private bool inputReleased = true; // Flag to check if the input has been released
    private int selectionIndex = 0; // 0: NameInput, 1: Save Score, 2: Restart
    private bool canAcceptInput = false; // Flag to check if input can be accepted

    void Start()
    {
        // Initialize all letters to 'A'
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].text = "A";
        }
        HighlightCurrentLetter();
    }

    void Update()
    {
        if (!requiredGameObject.activeInHierarchy)
        {
            canAcceptInput = false; // Reset input acceptance flag
            return; // Do nothing if the required GameObject is not active
        }

        if (!canAcceptInput)
        {
            StartCoroutine(WaitForInputDelay(0.35f)); // Start the coroutine to wait for the delay
        }
        else
        {
            HandleInput();
        }
    }

    IEnumerator WaitForInputDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAcceptInput = true;
    }

    void HandleInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (selectionIndex == 0)
        {
            // Handle letter input
            if (Input.GetKeyDown(KeyCode.JoystickButton1) && inputReleased) // X button
            {
                ChangeLetterForward();
                inputReleased = false;
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton2) && inputReleased) // Circle button
            {
                ChangeLetterBackward();
                inputReleased = false;
            }

            if (horizontalInput < -0.5f && inputReleased)
            {
                MoveToPreviousLetter();
                inputReleased = false;
            }

            if (horizontalInput > 0.5f && inputReleased)
            {
                MoveToNextLetter();
                inputReleased = false;
            }

            // Move to buttons when pressing down
            if (verticalInput < -0.5f && inputReleased)
            {
                selectionIndex = 1; // Move to Save Score button
                HighlightButton(0);
                inputReleased = false;
            }
            else if (verticalInput > 0.5f && inputReleased)
            {
                selectionIndex = 2; // Move to Restart button
                HighlightButton(1);
                inputReleased = false;
            }
        }
        else if (selectionIndex == 1)
        {
            // Handle Save Score button input
            if (Input.GetKeyDown(KeyCode.JoystickButton1) && inputReleased) // X button
            {
                saveScoreButton.onClick.Invoke();
                inputReleased = false;
            }

            // Navigate up and down between buttons and name input
            if (verticalInput < -0.5f && inputReleased)
            {
                selectionIndex = 2; // Move to Restart button
                HighlightButton(1);
                inputReleased = false;
            }
            else if (verticalInput > 0.5f && inputReleased)
            {
                selectionIndex = 0; // Move to Name Input
                HighlightCurrentLetter();
                inputReleased = false;
            }
        }
        else if (selectionIndex == 2)
        {
            // Handle Restart button input
            if (Input.GetKeyDown(KeyCode.JoystickButton1) && inputReleased) // X button
            {
                restartButton.onClick.Invoke();
                inputReleased = false;
            }

            // Navigate up and down between buttons and name input
            if (verticalInput < -0.5f && inputReleased)
            {
                selectionIndex = 0; // Move to Name Input
                HighlightCurrentLetter();
                inputReleased = false;
            }
            else if (verticalInput > 0.5f && inputReleased)
            {
                selectionIndex = 1; // Move to Save Score button
                HighlightButton(0);
                inputReleased = false;
            }
        }

        // Reset inputReleased flag when the joystick is neutral
        if (Mathf.Abs(horizontalInput) < 0.1f && Mathf.Abs(verticalInput) < 0.1f)
        {
            inputReleased = true;
        }
    }

    void ChangeLetterForward()
    {
        char currentChar = letters[currentLetterIndex].text[0];
        int newIndex = (System.Array.IndexOf(alphabet, currentChar) + 1) % alphabet.Length;
        letters[currentLetterIndex].text = alphabet[newIndex].ToString();
    }

    void ChangeLetterBackward()
    {
        char currentChar = letters[currentLetterIndex].text[0];
        int newIndex = (System.Array.IndexOf(alphabet, currentChar) - 1 + alphabet.Length) % alphabet.Length;
        letters[currentLetterIndex].text = alphabet[newIndex].ToString();
    }

    void MoveToPreviousLetter()
    {
        currentLetterIndex = (currentLetterIndex - 1 + letters.Length) % letters.Length;
        HighlightCurrentLetter();
    }

    void MoveToNextLetter()
    {
        currentLetterIndex = (currentLetterIndex + 1) % letters.Length;
        HighlightCurrentLetter();
    }

    void HighlightCurrentLetter()
    {
        // Add highlight effect to current letter
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].color = Color.white; // Reset to default color
        }

        if (selectionIndex == 0)
        {
            letters[currentLetterIndex].color = Color.yellow; // Change color to highlight
        }

        // Reset button colors
        SetButtonColors(Color.white, Color.black);
    }

    void HighlightButton(int index)
    {
        // Reset letters color to white
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].color = Color.white;
        }

        // Reset button colors
        SetButtonColors(Color.white, Color.black);

        // Highlight the selected button
        if (index == 0)
        {
            SetButtonColor(saveScoreButton, Color.yellow, Color.white);
        }
        else if (index == 1)
        {
            SetButtonColor(restartButton, Color.yellow, Color.white);
        }
    }

    void SetButtonColors(Color buttonColor, Color textColor)
    {
        SetButtonColor(saveScoreButton, buttonColor, textColor);
        SetButtonColor(restartButton, buttonColor, textColor);
    }

    void SetButtonColor(Button button, Color buttonColor, Color textColor)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = buttonColor;
        colorBlock.highlightedColor = buttonColor;
        colorBlock.pressedColor = buttonColor;
        colorBlock.selectedColor = buttonColor;
        button.colors = colorBlock;

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.color = textColor;
        }
    }
}
