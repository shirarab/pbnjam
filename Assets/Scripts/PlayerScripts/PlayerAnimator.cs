using System;
using System.Collections;
using System.Collections.Generic;
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


    public void TriggerAnimation(AnimationType animationToPlay)
    {
        animator.SetInteger("animationState", (int)animationToPlay);
    }



    private void Awake() 
    {
        this.animator = GetComponent<Animator>();
    }

}
