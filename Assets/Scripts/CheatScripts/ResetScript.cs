using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Score = ScoreScripts.Score;

public class ResetScript : MonoBehaviour
{
    public void ResetGame()
    {
        // Reload the scene
        GameManager.Instance.ResetGame();
    }

}
