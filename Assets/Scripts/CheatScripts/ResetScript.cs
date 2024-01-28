using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    public void ResetGame()
    {
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
