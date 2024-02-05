using UnityEngine;

public class Instruction : MonoBehaviour
{
    public void StartGame()
    {
        // disable the game canvas
        gameObject.SetActive(false);
        // reset the game
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("A key or mouse click has been detected");
            StartGame();
        }
    }
}
