using UnityEngine;
using TMPro;

public class InputFieldMirror : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text[] letterTexts;

    void Start()
    {
        if (inputField == null || letterTexts == null || letterTexts.Length != 5)
        {
            Debug.LogError("Please assign the InputField and exactly 5 TMP_Text objects in the inspector.");
            return;
        }

        // Set initial input field text to "AAAAA"
        inputField.text = "AAAAA";

        // Initialize letter texts to "A"
        for (int i = 0; i < letterTexts.Length; i++)
        {
            letterTexts[i].text = "A";
        }

        // Add listener to update letters when input field changes
        inputField.onValueChanged.AddListener(UpdateLetters);
    }

    void Update()
    {
        RefreshInputField();
    }

    void UpdateLetters(string input)
    {
        for (int i = 0; i < letterTexts.Length; i++)
        {
            if (i < input.Length)
            {
                letterTexts[i].text = input[i].ToString();
            }
            else
            {
                letterTexts[i].text = "A"; // Default to "A" if input is shorter
            }
        }
    }

    void RefreshInputField()
    {
        string newText = "";
        for (int i = 0; i < letterTexts.Length; i++)
        {
            newText += letterTexts[i].text;
        }
        inputField.text = newText;
    }
}
