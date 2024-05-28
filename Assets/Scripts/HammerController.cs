using UnityEngine;

public class HammerController : MonoBehaviour
{
    // The GameObject to enable/disable
    public GameObject targetGameObject;

    private EmissionController[] emissionControllers;

    private void Start()
    {
        // Initially set the targetGameObject to inactive
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Find all instances of EmissionController in the scene
        emissionControllers = FindObjectsOfType<EmissionController>();

        bool shouldActivate = false;

        // Check if any EmissionController has playerInTrigger set to true
        foreach (var controller in emissionControllers)
        {
            if (controller.playerInTrigger)
            {
                shouldActivate = true;
                break;
            }
        }

        // Set the targetGameObject active state based on shouldActivate
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(shouldActivate);
        }
    }
}
