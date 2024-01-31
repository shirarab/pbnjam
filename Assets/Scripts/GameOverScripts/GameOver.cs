using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void PlayAgain()
    {
        // disable the game over canvas
        gameObject.SetActive(false);
        // reset the game
        GameManager.Instance.ResetGame();
    }
    
    public void MainMenu()
    {
        // SceneManager.LoadScene("MainMenu");
        Application.Quit();
    }
}
