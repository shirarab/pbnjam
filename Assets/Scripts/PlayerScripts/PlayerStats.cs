using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region  Player Enums
enum LastDirection
{
    up,
    down,
    place
}

enum PlayerType
{
    PeanutButter,
    Jelly
};
#endregion


[System.Serializable]
public class PlayerStats 
{

    
    [SerializeField]
    private PlayerType playerType;
    internal PlayerType PlayerType { get => playerType;}

    
    #region Player Speed

    [SerializeField] //TODO: delete line after setting the idle start speed
    private float startSpeed;
    public float StartSpeed { get => startSpeed; set => startSpeed = value; }



    [SerializeField]
    private float Maxspeed;
    public float MaxSpeed { get => Maxspeed;}


    // [SerializeField]
    private float playerSpeed;
    public float PlayerSpeed{get; set;}


    

    [SerializeField]
    private float playerAccelRate;
    public float PlayerAccelRate { get => playerAccelRate; set => playerAccelRate = value; }


    [SerializeField]
    private float dirctChangeSpeed;
    public float DirctChangeSpeed { get => dirctChangeSpeed; set => dirctChangeSpeed = value; }


    #endregion


    #region Direction
    private float moveY;
    public float MoveY { get => moveY; set => moveY = value; }

    
    private LastDirection lastDirection;
    internal LastDirection LastDirection { get => lastDirection; set => lastDirection = value; }

    private Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }
    #endregion


    #region KeyControl
    [SerializeField] private KeyCode keyUp;
    public KeyCode KeyUp { get => keyUp;}


    [SerializeField] private KeyCode keyDown;
    public KeyCode KeyDown { get => keyDown;}

    #endregion

}
