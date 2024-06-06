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
        // Check if the F7 key is pressed and the requiredGameObject is active
        if (Input.GetKeyDown(KeyCode.F7) && requiredGameObject.activeInHierarchy)
        {
            // Select the input field if it's not null
            if (inputField != null)
            {
                inputField.Select();

                // Open the on-screen keyboard
                OpenOnScreenKeyboard();
            }
            else
            {
                Debug.LogWarning("TMP_InputField is not assigned in the Inspector.");
            }
        }
    }

    void OpenOnScreenKeyboard()
    {
        // Start the on-screen keyboard process
        Process oskProcess = new Process();
        oskProcess.StartInfo.FileName = "osk.exe";
        oskProcess.StartInfo.UseShellExecute = true;
        oskProcess.Start();
    }
}
