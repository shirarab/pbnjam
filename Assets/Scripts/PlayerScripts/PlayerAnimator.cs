using System.Collections;
using UnityEngine;

public enum AnimationType { Idle, Hit, Win, Lose};


public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    
    private AnimationType animationState;
    internal AnimationType AnimationState { get => animationState; set => animationState = value; }



    public PlayerAnimator(Animator anim)
    {
        animator = anim;
    }

    #region ANIMATOR METHODS----------------------------
    private void returnToIdleAnimation()
    {
        PlayAnimaion(AnimationType.Idle);
    }


    public void TriggerAnimation(AnimationType animationToPlay)
    {
        animator.SetInteger("animationState", (int)animationToPlay);
    }


    public IEnumerator BackToIdle(float durationOfHitAnimation)
    {
        yield return new WaitForSeconds(durationOfHitAnimation);
        returnToIdleAnimation();
    }


    public void PlayAnimaion(AnimationType newAnimation)
    {
        if(animator && AnimationState != newAnimation)
        {
        AnimationState = newAnimation;
        TriggerAnimation(AnimationState);
        }   
    }
    
    public void SetWinAnimation(bool isWinning)
    {
        animator.SetBool("winningAnimation", isWinning);
    }

    public void SetLoseAnimation(bool isLosing)
    {
        animator.SetBool("losingAnimation", isLosing);
    }
    #endregion


    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
    }

}
