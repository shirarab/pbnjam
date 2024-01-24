using UnityEngine;

public class SetResolutionScript : MonoBehaviour
{
    private const int SCREEN_REF_WIDTH = 1920;
    private const int SCREEN_REF_HEIGHT = 1080;
    
    private const float landscapeRatio =  SCREEN_REF_WIDTH / SCREEN_REF_HEIGHT;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Resolution, width: " + Screen.width + ", height: " + Screen.height);

        // Get the real ratio
        float ratio = (float)Screen.width / (float)Screen.height;

        // Cammera settings to landscape
        if (ratio >= landscapeRatio)
        {
            Camera.main.orthographicSize = 1080f / 200f;
        }
        else
        {
            float scaledHeight = 1920f / ratio;
            Camera.main.orthographicSize = scaledHeight / 200f;
        }
    }
}