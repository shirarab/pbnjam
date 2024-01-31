using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public enum BreadType
{
    Bread,
    PeanutButterBread,
    JellyBread,
    ToastBread
}



public class Bread : MonoBehaviour
{
    #region COMPONENTS
    [SerializeField] 
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody { get => rigidBody;}


    [SerializeField]
    private BoxCollider2D boxcollider2D;
    public BoxCollider2D BoxCollider2D { get => boxcollider2D;}



    private SpriteRenderer spriteRenderer;
    #endregion

    private Dictionary<int, BreadType> layersToBreadTypeDict;

    // STATS-----------------------------------------------
    // Bread,
    // PeanutButterBread,
    // JellyBread,
    // ToastBread
    



    [SerializeField]
    BreadType currentBreadType;
    internal BreadType CurrentBreadType { get => currentBreadType; set => currentBreadType = value; }


    [SerializeField]
    // NOTE: the order of the list is important
    private Sprite[] breadSprites;

    // STATS-----------------------------------------------

    private void Start()
    {
        layersToBreadTypeDict = new()
        {
            { LayerMask.NameToLayer(BreadType.Bread.ToString()), BreadType.Bread },
            { LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString()), BreadType.PeanutButterBread },
            { LayerMask.NameToLayer(BreadType.JellyBread.ToString()), BreadType.JellyBread }
        };

        // Check if there is a SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer(Constants.BREAD);


        // If not, add a SpriteRenderer component dynamically
        if (!spriteRenderer)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        // Set the sprite on the SpriteRenderer
        spriteRenderer.sprite = breadSprites[(int)currentBreadType];;
    }



    // TRIGER LOGIC------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (layersToBreadTypeDict.ContainsKey(other.gameObject.layer) && layersToBreadTypeDict[other.gameObject.layer] != currentBreadType)
        {
            // GameManager.Instance.IncrementScoreByBread(layersToBreadTypeDict[other.gameObject.layer], currentBreadType);
            UpdateBreadTypeAndSprite(other);
        }
    }



    void UpdateBreadTypeAndSprite(Collision2D other)
    {
        currentBreadType = layersToBreadTypeDict[other.gameObject.layer];
        
        spriteRenderer.sprite = breadSprites[(int)currentBreadType];
        UpdateBreadLayer(other.gameObject.layer);

    }


    void UpdateBreadLayer(int newLayer)
    {
        gameObject.layer = newLayer;
    }
    // TRIGER LOGIC------------------------
    
    public void ResetBread()
    {
        currentBreadType = BreadType.Bread;
        spriteRenderer.sprite = breadSprites[(int)currentBreadType];
        UpdateBreadLayer(LayerMask.NameToLayer(BreadType.Bread.ToString()));
    }
}
