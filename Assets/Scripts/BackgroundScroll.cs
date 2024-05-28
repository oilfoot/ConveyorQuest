using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public float speed = 2.0f;

    private Transform bg1Start;
    private Transform bg1End;
    private Transform bg2Start;
    private Transform bg2End;
    private float screenHalfWidth;

    void Start()
    {
        bg1Start = background1.transform.Find("BGStart1");
        bg1End = background1.transform.Find("BGEnd1");
        bg2Start = background2.transform.Find("BGStart2");
        bg2End = background2.transform.Find("BGEnd2");

        // Calculate the half-width of the screen based on the camera's orthographic size
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        // Move the backgrounds to the left
        background1.transform.position += Vector3.left * speed * Time.deltaTime;
        background2.transform.position += Vector3.left * speed * Time.deltaTime;

        // Check if the start of background1 is off screen
        if (bg1Start.position.x < Camera.main.transform.position.x - screenHalfWidth)
        {
            // Move background1 so that bg1End aligns with bg2Start
            float offset = bg2Start.position.x - bg1End.position.x;
            background1.transform.position += new Vector3(offset, 0, 0);
        }

        // Check if the start of background2 is off screen
        if (bg2Start.position.x < Camera.main.transform.position.x - screenHalfWidth)
        {
            // Move background2 so that bg2End aligns with bg1Start
            float offset = bg1Start.position.x - bg2End.position.x;
            background2.transform.position += new Vector3(offset, 0, 0);
        }
    }
}
