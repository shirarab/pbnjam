using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    #region PLAYER FIELDS ------------------------------
    [SerializeField] 
    PlayerComponents components;
    [SerializeField]
    InputAction moveAction;
    public PlayerComponents Components { get => components;}


    [SerializeField] 
    PlayerStats stats;
    public PlayerStats Stats { get => stats;}


    [SerializeField] 
    private PlayerAnimator playerAnimator; 
    public PlayerAnimator PlayerAnimator { get => this.playerAnimator; set => this.playerAnimator = value; }
    private Vector2 _moveInput;
    #endregion



    #region METHOD FLAGS AND HELPERS ------------------
    private bool twoKeysFlag;
    
    [SerializeField] 
    AnimationType animationHelper;
    private bool isPlayed;//flag for checking if the hit abhmation played

    
    #endregion
    
    #region ANIMATION ------------------------------------

    private void PlayAnimaion(AnimationType newAnimation)
    {
        if(playerAnimator && playerAnimator.AnimationState != newAnimation)
        {
        playerAnimator.AnimationState = newAnimation;
        playerAnimator.TriggerAnimation(playerAnimator.AnimationState);
        }   
    }

    // TODO: need to update the animator window to exit after playing the hit animation
    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Check if the colliding GameObject has a specific tag.
        if (other.gameObject.CompareTag("ball"))
        {
            PlayAnimaion(AnimationType.Hit);
        }
        
    }

    private void returnToIdleAnimation()
    {
        PlayAnimaion(AnimationType.Idle);

    }

    private bool isAnimationPlayed()
    {
         // Animator Panimator = playerAnimator.GetComponent<Animator>();
         // AnimatorStateInfo stateInfo = Panimator.GetCurrentAnimatorStateInfo(0);
    
        // return stateInfo.IsName("hit") && stateInfo.normalizedTime >= 1.0f;
        return false;
    }
    #endregion


    #region START, UPDATE --------------------------------
    // Start is called before the first frame update
    void Start()
    {
        Stats.PlayerSpeed = Stats.StartSpeed; 
        Stats.MoveY = 0; 
        twoKeysFlag = false;
        moveAction.Enable();
        // playerAnimator.AnimationState = AnimationType.Idle;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _moveInput = moveAction.ReadValue<Vector2>();
        Vector2 force = new Vector2(_moveInput.x * Stats.StartSpeed, _moveInput.y * Stats.StartSpeed);
        Components.RigidBody.AddForce(force);
    }
    
    private void stopMotion()
    {
        if (Input.GetKeyUp(Stats.KeyUp))
        {
            Components.RigidBody.velocity = Vector2.zero;   
        }

        if (Input.GetKeyUp(Stats.KeyDown))
        {
            Components.RigidBody.velocity = Vector2.zero;   
        }
    }


    // Update is called once per frame
    void Update()
    {
        stopMotion();
        // TODO: check if can do this in animator window
        if(playerAnimator && playerAnimator.AnimationState == AnimationType.Hit)
        {
            isPlayed = isAnimationPlayed();
            if(isPlayed){returnToIdleAnimation();}
            // isPlayed = false;
        }
    }
    #endregion
}
