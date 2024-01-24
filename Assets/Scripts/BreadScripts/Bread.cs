using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BreadType
{
    Bread,
    PeanutButterBread,
    JellyBread,
    ToastBread
}



public class Bread : MonoBehaviour
{
    // COMPONENTS------------------------------------------
    [SerializeField] 
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody { get => rigidBody;}


    [SerializeField]
    private BoxCollider2D boxcollider2D;
    public BoxCollider2D BoxCollider2D { get => boxcollider2D;}



    private SpriteRenderer spriteRenderer;
    // COMPONENTS------------------------------------------



    // STATS-----------------------------------------------
    [SerializeField]
    BreadType currentBreadType;
    internal BreadType CurrentBreadType { get => currentBreadType; set => currentBreadType = value; }


    [SerializeField]
    // NOTE: the order of the list is important
    private Sprite[] breadSprites;



    [SerializeField]
    private Sprite currentSprite;
    public Sprite CurrentSprite { get => currentSprite; set => currentSprite = value; }
    // STATS-----------------------------------------------

    private void Start()
     {
    // Check if there is a SpriteRenderer component
    spriteRenderer = GetComponent<SpriteRenderer>();
    
    // If not, add a SpriteRenderer component dynamically
    if (!spriteRenderer)
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    // NOTE: defined in unity
    currentSprite = breadSprites[(int)currentBreadType];
    // Set the sprite on the SpriteRenderer
    spriteRenderer.sprite = currentSprite;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
