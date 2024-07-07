using UnityEngine;
using TMPro;
using System.Diagnostics; // For starting external processes
using Debug = UnityEngine.Debug; // Resolves ambiguity

public class SelectTMPInputField : MonoBehaviour
{
    // Public variable to assign the TMP_InputField in the Inspector
    public TMP_InputField inputField;
    // Public variable to assign the GameObject in the Inspector
    public GameObject requiredGameObject;

    void Update()
    {
        // Check if the L1 button on the PS4 controller is pressed and the requiredGameObject is active
        if (Input.GetKeyDown(KeyCode.JoystickButton4) && requiredGameObject.activeInHierarchy)
        {
            // Select the input field if it's not null
            if (inputField != null)
            {
                inputField.Select();
            }
            else
            {
                Debug.LogWarning("TMP_InputField is not assigned in the Inspector.");
            }
        }
    }
}
