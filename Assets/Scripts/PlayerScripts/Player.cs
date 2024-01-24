using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Player player;

    // PLAYER FIELDS ------------------------------
    [SerializeField] 
    PlayerComponents components;
    public PlayerComponents Components { get => components;}



    [SerializeField] 
    PlayerStats stats;
    public PlayerStats Stats { get => stats;}
    // PLAYER FIELDS ------------------------------



    
    private void setDirection(int yDirection, LastDirection newDirection)
    {
        Stats.MoveY = yDirection;
        Stats.LastDirection = newDirection;
    }


    public void HandleInput()
    {
        // Debug.Log("enter -> HandleInput");
        if(Input.GetKey(Stats.KeyUp) && Input.GetKey(Stats.KeyDown))
        {
            if(Stats.LastDirection == LastDirection.up)
            {
                Stats.MoveY = -1;
            }
            else
            {
                Stats.MoveY = 1;               
            }
        }

        else if(Input.GetKey(Stats.KeyUp))
        {
            
            setDirection(1,  LastDirection.up);
        }
        else if(Input.GetKey(Stats.KeyDown))
        {
            setDirection(-1, LastDirection.down);
        } 
        
        else
        {
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
        new Vector2(Components.RigidBody.velocity.x, Stats.Direction.y * Stats.playerSpeed * Time.deltaTime);

    if (Stats.Direction.y != 0)
    {
        // Handle move direction image for player
        // Assuming you don't want to flip along the Y-axis
        transform.localScale = new Vector3(1, Stats.Direction.y < 0 ? -1 : 1, 1);
    }
}







    // Start is called before the first frame update
    void Start()
    {
        stats.playerSpeed = stats.Speed; 
        stats.MoveY = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();//procesinput||parser
        Move(transform);  
    }
}
