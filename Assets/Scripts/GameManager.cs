using BreadScripts;

public class GameManager : Singleton<GameManager>
{
    public BreadGrid breadGrid;
    
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        breadGrid.GenerateGrid();
    }
}
