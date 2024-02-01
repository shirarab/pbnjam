using UnityEngine;

public class ResetScript : MonoBehaviour
{
    public void ResetGame()
    {
        // Reload the scene
        GameManager.Instance.ResetGame();
    }

}
