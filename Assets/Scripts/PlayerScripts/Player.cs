using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player player;

    #region PLAYER FIELDS ------------------------------
    [SerializeField] 
    PlayerComponents components;
    public PlayerComponents Components { get => components;}



    [SerializeField] 
    PlayerStats stats;
    public PlayerStats Stats { get => stats;}
    #endregion
    
    
    private void setDirection(int yDirection, LastDirection newDirection)
    {
        Stats.MoveY = yDirection;
        Stats.LastDirection = newDirection;
        Stats.PlayerSpeed = Stats.StartSpeed;
    }



    private void setMoveYandPlayerSpwwd(int yDirection)
    {
        Stats.MoveY = yDirection;
        Stats.PlayerSpeed = Stats.StartSpeed * Stats.DirctChangeSpeed;
    }



    public void HandleInput()
    {
        
        if(Input.GetKey(Stats.KeyUp) && Input.GetKey(Stats.KeyDown))
        {
            if(Stats.LastDirection == LastDirection.up)
            {
                setMoveYandPlayerSpwwd(-1);
            }
            if(Stats.LastDirection == LastDirection.down)
            {
                setMoveYandPlayerSpwwd(1);
            }
        }

        else if(Input.GetKey(Stats.KeyUp))
        {
            if(Stats.LastDirection != LastDirection.up)
            {
                setDirection(1,  LastDirection.up);
            }
        }
        else if(Input.GetKey(Stats.KeyDown))
        {
            if(Stats.LastDirection != LastDirection.down)
            {
                setDirection(-1, LastDirection.down);
            }
        } 
        
        else
        {
            Stats.PlayerSpeed = Stats.StartSpeed;
            Stats.MoveY = 0;
            Stats.LastDirection = LastDirection.place;
        } 

        // only horizontal move -> Y cordinate only 
        Stats.Direction = 
            new Vector2(Components.RigidBody.velocity.x, Stats.MoveY);
    }


    public void Move(Transform transform)
    {

    Components.RigidBody.velocity = 
        new Vector2(Components.RigidBody.velocity.x, Stats.Direction.y * Stats.PlayerSpeed * Time.deltaTime);
    

    if (Stats.Direction.y != 0)
    {
        float speedFlag = (Stats.PlayerSpeed*Stats.PlayerAccelRate);

        if(speedFlag <= Stats.MaxSpeed)
        {
            Stats.PlayerSpeed *= Stats.PlayerAccelRate;
        }
        // add the player speed the diff betwin maxSpeed and playerSpeed
        else
        {
            Stats.PlayerSpeed = Stats.MaxSpeed;
        }
        // Handle move direction image for player
        // Assuming you don't want to flip along the Y-axis
        transform.localScale = new Vector3(1, Stats.Direction.y < 0 ? -1 : 1, 1);
    }
    }


    // Start is called before the first frame update
    void Start()
    {
        Stats.PlayerSpeed = Stats.StartSpeed; 
        Stats.MoveY = 0; 
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();//procesinput||parser
        Move(transform);  
    }
}
