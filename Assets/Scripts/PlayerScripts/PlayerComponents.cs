using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerComponents 
{
    
    [SerializeField] 
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody { get => rigidBody;}


    // [SerializeField] 
    // private PlayerAnimator animator;
    // public PlayerAnimator Animator { get => animator;}


    [SerializeField]
    private BoxCollider2D boxcollider2D;
    public BoxCollider2D BoxCollider2D { get => boxcollider2D;}


}
