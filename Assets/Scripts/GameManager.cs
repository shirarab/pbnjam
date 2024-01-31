using System;
using System.Collections;
using BreadScripts;
using ScoreScripts;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public BreadGrid breadGrid;
    [SerializeField]
    private Score scoreManager;
    [SerializeField]
    private Ball pbBall;
    [SerializeField]
    private Ball jamBall;
    [SerializeField]
    private Canvas PbGameOverCanvas;
    [SerializeField]
    private Canvas JamGameOverCanvas;
    [SerializeField] 
    private float gameEndWaitTime = 1f;
    [SerializeField]
    private float ballsActivationWaitTime = 0.5f;
    
    private bool isGameOver = false;
    
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        breadGrid.GenerateGrid();
        StartCoroutine(DelayBallsActivation());
    }
    
    public void IncrementScore(PlayerType playerType)
    {
        scoreManager.AddPoints(1, playerType);
    }
    
    public void DecrementScore(PlayerType playerType)
    {
        if (isGameOver) return;   
        scoreManager.RemovePoints(1, playerType);
    }

    public void IncrementScoreByBread(BreadType ballType, BreadType breadType)
    {
        if (isGameOver) return;
        var pointToAdd = 0;
        if (breadType == ballType)
        {
            pointToAdd = 0;
        }
        else if (breadType == BreadType.Bread)
        {
            pointToAdd = 1;
        }
        else if (breadType == BreadType.ToastBread)
        {
            // todo something
        }
        else if (breadType != ballType)
        {
            pointToAdd = 2;
        }

        var playerType = ballType == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
        scoreManager.AddPoints(pointToAdd, playerType);
    }
    
    public void IsGameOver(PlayerType playerType, int currentScore, int maxScore)
    {
        if (currentScore >= maxScore)
        {
            isGameOver = true;
            StartCoroutine(EndGame(playerType));
        }
    }

    private IEnumerator EndGame(PlayerType playerType)
    {
        yield return new WaitForSeconds(gameEndWaitTime);
        if (playerType == PlayerType.Jelly)
        {
            JamGameOverCanvas.gameObject.SetActive(true);
        }
        else
        {
            PbGameOverCanvas.gameObject.SetActive(true);
        }
        pbBall.gameObject.SetActive(false);
        jamBall.gameObject.SetActive(false);
    }
    
    private IEnumerator DelayBallsActivation()
    {
        yield return new WaitForSeconds(ballsActivationWaitTime);
        pbBall.gameObject.SetActive(true);
        jamBall.gameObject.SetActive(true);
        pbBall.ResetBall();
        jamBall.ResetBall();
    }

    public void ResetGame()
    {
        isGameOver = false;
        scoreManager.ResetScore();
        breadGrid.ResetGrid();
        StartCoroutine(DelayBallsActivation());
    }
}
