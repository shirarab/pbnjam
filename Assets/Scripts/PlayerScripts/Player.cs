using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    #region PLAYER FIELDS ------------------------------
    [SerializeField] 
    PlayerComponents components;
    public PlayerComponents Components { get => components;}



    [SerializeField] 
    PlayerStats stats;
    public PlayerStats Stats { get => stats;}


    [SerializeField] 
    private PlayerAnimator playerAnimator; // Added PlayerAnimator field
    public PlayerAnimator PlayerAnimator { get => this.playerAnimator; set => this.playerAnimator = value; }
    #endregion



    #region METHOD FLAGS AND HELPERS ------------------
    private bool twoKeysFlag;
    
    [SerializeField] 
    AnimationType animationHelper;
    
    #endregion
    

    #region STATS SETTERS ------------------------------
    private void setDirection(int yDirection, LastDirection newDirection)
    {
        Stats.MoveY = yDirection;
        Stats.LastDirection = newDirection;
        Stats.PlayerSpeed = Stats.StartSpeed;
    }

    private void setMoveYandPlayerSpeed(int yDirection)
    {
        Stats.MoveY = yDirection;
        Stats.PlayerSpeed = Stats.StartSpeed * Stats.DirctChangeSpeed;
    }
    #endregion

    
    #region INPUT HANDLE -------------------------------
    public void HandleInput()
    {
        
        if(Input.GetKey(Stats.KeyUp) && Input.GetKey(Stats.KeyDown))
        {
            twoKeysFlag = true;
            if(Stats.LastDirection == LastDirection.up)
            {
                setMoveYandPlayerSpeed(-1);
            }
            if(Stats.LastDirection == LastDirection.down)
            {
                setMoveYandPlayerSpeed(1);
            }
        }

        else if(Input.GetKey(Stats.KeyUp))
        {
            if(Stats.LastDirection != LastDirection.up || twoKeysFlag)
            {
                setDirection(1,  LastDirection.up);
                twoKeysFlag = false;
            }
        }
        else if(Input.GetKey(Stats.KeyDown))
        {
            if(Stats.LastDirection != LastDirection.down || twoKeysFlag)
            {
                setDirection(-1, LastDirection.down);
                twoKeysFlag = false;
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
    #endregion

    

    #region MOVE ---------------------------------------
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
    #endregion


    #region START, UPDATE --------------------------------
    // Start is called before the first frame update
    void Start()
    {
        Stats.PlayerSpeed = Stats.StartSpeed; 
        Stats.MoveY = 0; 
        twoKeysFlag = false;
        
        playerAnimator.AnimationState = AnimationType.Idle;
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();//procesinput||parser
        Move(transform);  
        

        if(playerAnimator.AnimationState != animationHelper)
        {
        playerAnimator.AnimationState = animationHelper;
        playerAnimator.TriggerAnimation(playerAnimator.AnimationState);
        }
    }
    #endregion
}
