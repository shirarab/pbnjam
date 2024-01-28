using BreadScripts;
using ScoreScripts;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public BreadGrid breadGrid;
    [SerializeField]
    private Score scoreManager;
    
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        breadGrid.GenerateGrid();
    }
    
    public void IncrementScore(PlayerType playerType)
    {
        scoreManager.AddPoints(1, playerType);
    }
}
