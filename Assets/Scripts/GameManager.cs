using System.Collections;
using System.Linq;
using BreadScripts;
using ScoreScripts;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public BreadGrid breadGrid;
    [SerializeField] private Score scoreManager;
    [SerializeField] private Ball pbBall;
    [SerializeField] private Ball jamBall;
    [SerializeField] private Canvas PbGameOverCanvas;
    [SerializeField] private Canvas JamGameOverCanvas;
    [SerializeField] private float gameEndWaitTime = 1f;
    [SerializeField] private float ballsActivationWaitTime = 0.5f;
    [SerializeField] public float gameTime = 60.0f;
    [SerializeField] private float extraGameTime = 10.0f;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private PlayerAnimator pbPlayerAnimator;
    [SerializeField] private PlayerAnimator jamPlayerAnimator;
    [SerializeField] private ToastSpawner toastSpawner;
    [SerializeField] private Timer timer;
    public AudioClip endSound;
    public AudioClip timerSound;
    
    private bool isGameOver = false;


    public void StartGame()
    {
        // initialize the game
        breadGrid.GenerateGrid();
        scoreManager.SetNumberOfBreads(breadGrid.GetNumberOfBreads());
        StartCoroutine(toastSpawner.SpawnToast());
        StartCoroutine(DelayBallsActivation());
        StartCoroutine(GameTimer(gameTime + ballsActivationWaitTime));
    }

    public void HandleGoalToPlayer(PlayerType player)
    {
        var breadToReset = player == PlayerType.Jelly ? BreadType.JellyBread : BreadType.PeanutButterBread;
        var resetCount = breadGrid.ResetBreadType(breadToReset);
        var toRemove = breadToReset == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
        scoreManager.RemovePoints(resetCount, toRemove);
    }
    
    public void PlaySound(AudioClip sound)
    {
        sfx.PlayOneShot(sound);
    }

    public void PlayTimerSound()
    {
        sfx.PlayOneShot(timerSound);
    }

    public void UpdateScoreByBread(BreadType ballType, BreadType breadType)
    {
        PlayerType? toAdd = null;
        PlayerType? toRemove = null;

        if (breadType == BreadType.Bread)
        {
            toAdd = ballType == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
        }
        else if (breadType == BreadType.ToastBread)
        {
            // todo something
        }
        else if (breadType != ballType)
        {
            toAdd = ballType == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
            toRemove = breadType == BreadType.JellyBread ? PlayerType.Jelly : PlayerType.PeanutButter;
        }

        if (toAdd != null) scoreManager.AddPoints(1, toAdd.Value);
        if (toRemove != null) scoreManager.RemovePoints(1, toRemove.Value);
    }

    public void ResetGame()
    {
        isGameOver = false;
        scoreManager.ResetScore();
        breadGrid.ResetGrid();
        StartCoroutine(DelayBallsActivation());
        StartCoroutine(GameTimer(gameTime + ballsActivationWaitTime, isRestart: true));
        StartCoroutine(toastSpawner.SpawnToast());
    }


    private IEnumerator SetWinner(PlayerType winner)
    {
        // EventManager.StartEndOfGame((int)winner);
        PlaySound(endSound);
        DisableObjects(winner);
        if (winner==PlayerType.Jelly)
        {
            jamPlayerAnimator.SetWinAnimation(true);
            pbPlayerAnimator.SetLoseAnimation(true);
        }
        else
        {
            pbPlayerAnimator.SetWinAnimation(true);
            jamPlayerAnimator.SetLoseAnimation(true);
        }
        yield return new WaitForSeconds(gameEndWaitTime);

        if (winner == PlayerType.Jelly)
        {
            JamGameOverCanvas.gameObject.SetActive(true);
            jamPlayerAnimator.SetWinAnimation(false);
            pbPlayerAnimator.SetLoseAnimation(false);
        }
        else
        {
            PbGameOverCanvas.gameObject.SetActive(true);
            pbPlayerAnimator.SetWinAnimation(false);
            jamPlayerAnimator.SetLoseAnimation(false);
        }
    }
    
    private void DisableObjects(PlayerType winner)
    {
        pbBall.gameObject.SetActive(false);
        jamBall.gameObject.SetActive(false);
        toastSpawner.gameObject.SetActive(false);
        if (winner==PlayerType.Jelly)
        {
            jamPlayerAnimator.SetWinAnimation(false);
            pbPlayerAnimator.SetLoseAnimation(false);
        }
        else
        {
            pbPlayerAnimator.SetWinAnimation(false);
            jamPlayerAnimator.SetLoseAnimation(false);
        }
        timer.StopTimer(); // not sure needed here
    }

    private IEnumerator DelayBallsActivation()
    {
        yield return new WaitForSeconds(ballsActivationWaitTime);
        pbBall.gameObject.SetActive(true);
        jamBall.gameObject.SetActive(true);
        pbBall.ResetBall();
        jamBall.ResetBall();
    }

    private IEnumerator GameTimer(float time, bool isExtraTime = false, bool isRestart = false)
    {
        while (true)
        {
            Debug.Log("Game Timer: " + time);
            if (isExtraTime)
            {
                timer.AddSecondsToTimer(time);
            }
            timer.StartTimer(isRestart);
            yield return new WaitForSeconds(time);
            Debug.Log("Game Over");
            timer.StopTimer();
            EndGame();
        }
    }

    private void EndGame()
    {
        var breadCounts = breadGrid.GetBreadCountsByType();
        PlayerType winnerType;
        var pbCount = breadCounts[BreadType.PeanutButterBread];
        var jamCount = breadCounts[BreadType.JellyBread];
        if (pbCount > jamCount)
        {
         winnerType = PlayerType.PeanutButter;   
        }
        else if (jamCount > pbCount)
        {
            winnerType = PlayerType.Jelly;
        }
        else
        {
            StartCoroutine(GameTimer(extraGameTime, isExtraTime: true));
            return;
        }
        StartCoroutine(SetWinner(winnerType));
    }
}