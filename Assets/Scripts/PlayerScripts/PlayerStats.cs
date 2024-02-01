using UnityEngine;


#region  Player Enums
enum LastDirection
{
    up,
    down,
    place
}

public enum PlayerType
{
    PeanutButter,
    Jelly
};
#endregion


[System.Serializable]
public class PlayerStats 
{

    #region Player Type
    [SerializeField]
    private PlayerType playerType;
    internal PlayerType PlayerType { get => playerType;}
    # endregion
    
    #region Player Speed
    [SerializeField] //TODO: delete line after setting the idle start speed
    private float startSpeed = 5;
    public float StartSpeed { get => startSpeed; set => startSpeed = value; }
    #endregion

    #region KeyControl
    [SerializeField] private KeyCode keyUp;
    public KeyCode KeyUp { get => keyUp;}


    [SerializeField] private KeyCode keyDown;
    public KeyCode KeyDown { get => keyDown;}

    #endregion

}
