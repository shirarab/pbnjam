using System.Collections;
using System.Collections.Generic;
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
}
