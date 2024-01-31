using System.Collections;
using System.Linq;
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

    [SerializeField] private float gameTime = 60.0f;
    [SerializeField] private float extraGameTime = 10.0f;
    
    private bool isGameOver = false;
    
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        breadGrid.GenerateGrid();
        StartCoroutine(DelayBallsActivation());
        StartCoroutine(GameTimer(gameTime));
    }
    
    public void HandleGoalToPlayer(PlayerType player)
    {
        var breadToReset = player == PlayerType.Jelly ? BreadType.JellyBread : BreadType.PeanutButterBread;
        breadGrid.ResetBreadType(breadToReset);
    }
    
    // public void IncrementScore(PlayerType playerType)
    // {
    //     scoreManager.AddPoints(1, playerType);
    // }
    //
    // public void DecrementScore(PlayerType playerType)
    // {
    //     if (isGameOver) return;   
    //     scoreManager.RemovePoints(1, playerType);
    // }
    //
    // public void IncrementScoreByBread(BreadType ballType, BreadType breadType)
    // {
    //     if (isGameOver) return;
    //     var pointToAdd = 0;
    //     if (breadType == ballType)
    //     {
    //         pointToAdd = 0;
    //     }
    //     else if (breadType == BreadType.Bread)
    //     {
    //         pointToAdd = 1;
    //     }
    //     else if (breadType == BreadType.ToastBread)
    //     {
    //         // todo something
    //     }
    //     else if (breadType != ballType)
    //     {
    //         pointToAdd = 1;
    //     }
    //
    //     var playerType = ballType == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
    //     scoreManager.AddPoints(pointToAdd, playerType);
    // }
    
    // public void IsGameOver(PlayerType playerType, int currentScore, int maxScore)
    // {
    //     if (currentScore >= maxScore)
    //     {
    //         isGameOver = true;
    //         StartCoroutine(SetWinner(playerType));
    //     }
    // }
    
    public void ResetGame()
    {
        isGameOver = false;
        scoreManager.ResetScore();
        breadGrid.ResetGrid();
        StartCoroutine(DelayBallsActivation());
    }

    private IEnumerator SetWinner(PlayerType playerType)
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
    
    private IEnumerator GameTimer(float time) {
        while(true) {
            yield return new WaitForSeconds(time);
            EndGame();
        }
    }

    private void EndGame()
    {
        var breadCounts = breadGrid.GetBreadCounts();
        var winnerBread = breadCounts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        if (winnerBread != BreadType.JellyBread && winnerBread != BreadType.PeanutButterBread)
        {
            StartCoroutine(GameTimer(extraGameTime));
            return;
        }
        
        var winner = winnerBread == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
        StartCoroutine(SetWinner(winner));
    }
}
