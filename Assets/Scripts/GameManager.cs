using UnityEngine;

public enum GameState
{
    Start,
    GamePlay,
    GameOver,
    // Restart,
    // EndGame
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState = GameState.Start;

    private void PlayGame()
    {
        gameState = GameState.GamePlay;
        // startscene.SetActive(false);
        // gameplayscene.SetActive(true);
        // ScoreScripts.Score.Instance.ResetPoints();
    }
    
    private void GameOver()
    {
        Debug.Log("GameManager: GameOver started.");

        gameState = GameState.GameOver;
        // gameplayscene.SetActive(false);
        // gameoverscene.SetActive(true);
    }
}
