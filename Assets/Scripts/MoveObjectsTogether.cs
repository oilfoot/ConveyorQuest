using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsTogether : MonoBehaviour
{
    // Radius within which to check proximity
    public float proximityRadius = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects tagged as BGStart and BGEnd
        GameObject[] bgStarts = GameObject.FindGameObjectsWithTag("BGStart");
        GameObject[] bgEnds = GameObject.FindGameObjectsWithTag("BGEnd");

        // Check each BGStart against each BGEnd
        foreach (GameObject start in bgStarts)
        {
            foreach (GameObject end in bgEnds)
            {
                // Calculate distance between current BGStart and BGEnd
                float distance = Vector3.Distance(start.transform.position, end.transform.position);

                // If they are within the specified radius, move BGStart to BGEnd's position
                if (distance <= proximityRadius)
                {
                    start.transform.position = end.transform.position;
                }
            }
        }
    }
}
