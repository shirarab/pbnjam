using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



[System.Serializable]
public class PlayerStats 
{
    private LastDirection lastDirection;
    internal LastDirection LastDirection { get => lastDirection; set => lastDirection = value; }

    



    [SerializeField]
    private PlayerType playerType;
    internal PlayerType PlayerType { get => playerType;}

    

    [SerializeField]
    private float speed;
    public float Speed { get => speed;}




    [SerializeField]
    public float playerSpeed{get; set;}


    private float moveY;
    public float MoveY { get => moveY; set => moveY = value; }


    [SerializeField] //TODO: delete this line
    private Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }



    
    [SerializeField] private KeyCode keyUp;
    public KeyCode KeyUp { get => keyUp;}

    [SerializeField] private KeyCode keyDown;
    public KeyCode KeyDown { get => keyDown;}

    

}
