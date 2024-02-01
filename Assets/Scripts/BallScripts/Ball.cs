using System;
using System.Collections;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private Sprite peanutButterBallSprite;
    
    [SerializeField]
    private Sprite jamBallSprite;
    
    [SerializeField]
    private float ballOffScreenWaitTime = 1f;

    [SerializeField] private float timeToWaitForSelfGoal = 3.0f;
    
    [SerializeField]
    private PlayerType currentLastPlayerType;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerType lastPlayerHit;
    private Vector2 startBallPosition;
    private Sprite startBallSprite;
    private int jellyLayer;
    private int pbLayer;
    private Sprite newBallSprite;
    private Vector2 ballDirection;

    private void Awake()
    {
        startBallPosition = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startBallSprite = newBallSprite = spriteRenderer.sprite;
        jellyLayer = LayerMask.NameToLayer(BreadType.JellyBread.ToString());
        pbLayer = LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString());
        InitializeGravity();
        LaunchBall();
    }
    
    private void InitializeGravity()
    {
        // if ball layer is jelly layer, then gravity is set to right, else gravity is set to left
        if (gameObject.layer == jellyLayer)
        {
            ballDirection = new Vector2(-1, 0f);
        }
        else if (gameObject.layer == pbLayer)
        {
            ballDirection = new Vector2(1, 0f);
        }
        
    }
    
    public Vector2 getBallDirection()
    {
        return ballDirection;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    // to fix changing sprites for an object that has animator
    private void LateUpdate()
    {
        if (spriteRenderer.sprite != newBallSprite)
        {
            spriteRenderer.sprite = newBallSprite;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag: Constants.PLAYER))
        {
            lastPlayerHit = collision.gameObject.GetComponent<Player>().Stats.PlayerType;
            if (lastPlayerHit != currentLastPlayerType && lastPlayerHit == PlayerType.Jelly)
            {
                UpdateLayer(jellyLayer);
                InitializeGravity();
                // untested yet
                newBallSprite = jamBallSprite;
            }
            else if (lastPlayerHit != currentLastPlayerType && lastPlayerHit== PlayerType.PeanutButter)
            {
                UpdateLayer(pbLayer);
                // untested yet
                newBallSprite = peanutButterBallSprite;
                InitializeGravity();
            }
        }
    }
    
    void LaunchBall()
    {
        // rb.velocity = ballDirection * speed;
        rb.AddForce(ballDirection * speed);
    }

    private void UpdateLayer(int newLayer)
    {
        gameObject.layer = newLayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ScoredGoal(other.gameObject.tag));
    }

    public void ResetBall()
    {
        transform.position = startBallPosition;
        newBallSprite = startBallSprite;
        LaunchBall();
    }
    
    private IEnumerator ScoredGoal(string tag)
    {
        var timeToWait = 0f;
        
        Debug.Log("before wait");
        yield return new WaitForSeconds(ballOffScreenWaitTime);
        Debug.Log(tag + " scored a goal!");
        if (tag.Equals(Constants.JAM_GOAL) && gameObject.layer == pbLayer)
        {
            GameManager.Instance.HandleGoalToPlayer(PlayerType.Jelly);
        }
        else if (tag.Equals(Constants.PEANUT_BUTTER_GOAL) && gameObject.layer == jellyLayer)
        {
            GameManager.Instance.HandleGoalToPlayer(PlayerType.PeanutButter);
        }
        else if ((tag.Equals(Constants.JAM_GOAL) && gameObject.layer == jellyLayer) ||
                 (tag.Equals(Constants.PEANUT_BUTTER_GOAL) && gameObject.layer == pbLayer))
        {
            timeToWait = timeToWaitForSelfGoal;
        }

        Invoke(nameof(ResetBall), timeToWait);
    }
}
