using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Canvas instructionsCanvas;
    [SerializeField]
    private float instructionsWaitTime = 4f;
    public void StartGame()
    {
        instructionsCanvas.gameObject.SetActive(true);
        StartCoroutine(DelayInstructions());
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    private IEnumerator DelayInstructions()
    {
        yield return new WaitForSeconds(instructionsWaitTime);
        SceneManager.LoadScene("Game");
    }
}
