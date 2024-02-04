using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private const string AnimationTriggerFormat = "{0}To{1}";

    #region COMPONENTS -------------------------------------
    [SerializeField] 
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody { get => rigidBody;}


    [SerializeField]
    private BoxCollider2D boxcollider2D;
    public BoxCollider2D BoxCollider2D { get => boxcollider2D;}



    private SpriteRenderer spriteRenderer;
    #endregion


    #region BREAD DATA -------------------------------------
    private Dictionary<int, BreadType> layersToBreadTypeDict;


    [SerializeField]
    BreadType currentBreadType;
    internal BreadType CurrentBreadType { get => currentBreadType; set => currentBreadType = value; }


    [SerializeField]
    // NOTE: the order of the list is important
    private Sprite[] breadSprites;
    
    [SerializeField] private Animator animator;
    #endregion


    #region MONO BEHAVIOUR ---------------------------------
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

    #endregion



    #region CLASS METHODS ----------------------------------
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (layersToBreadTypeDict.ContainsKey(other.gameObject.layer) && layersToBreadTypeDict[other.gameObject.layer] != currentBreadType)
        {
            if(currentBreadType == BreadType.ToastBread)
            {
                Destroy(gameObject);
            }
            else
            {
            GameManager.Instance.UpdateScoreByBread(layersToBreadTypeDict[other.gameObject.layer], currentBreadType);
            // GameManager.Instance.IncrementScoreByBread(layersToBreadTypeDict[other.gameObject.layer], currentBreadType);
            UpdateBreadTypeAndSprite(other);   
            }
        }
    }




    void UpdateBreadTypeAndSprite(Collision2D other)
    {
        var newBreadType = layersToBreadTypeDict[other.gameObject.layer];
        var animationTriggerName = string.Format(AnimationTriggerFormat, currentBreadType.ToString(), newBreadType.ToString());
        animator.SetTrigger(animationTriggerName);
        
        currentBreadType = newBreadType;
        UpdateBreadLayer(other.gameObject.layer);
    }




    void UpdateBreadLayer(int newLayer)
    {
        gameObject.layer = newLayer;
    }
    
    
    public void ResetBread()
    {
        var newBreadType = BreadType.Bread;
        var animationTriggerName = string.Format(AnimationTriggerFormat, currentBreadType.ToString(), newBreadType.ToString());
        
        currentBreadType = BreadType.Bread;
        animator.SetTrigger(animationTriggerName);
        UpdateBreadLayer(LayerMask.NameToLayer(BreadType.Bread.ToString()));
    }
    #endregion
}
