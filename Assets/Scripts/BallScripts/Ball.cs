using System.Collections;
using UnityEngine;
using Utils;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float ballOffScreenWaitTime = 1f;
    
    [SerializeField] private float launchDelay = 2.0f;
    
    [SerializeField]
    private PlayerType ballOfPlayerType;
    
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private PlayerType lastPlayerHit;
    private Vector2 startBallPosition;
    private int startBallLayer;
    private int jellyLayer;
    private int pbLayer;
    private Sprite newBallSprite;
    private Vector2 ballDirection;
    private Vector2 originalBallDirection;
    private bool isJamBall;

    private void Awake()
    {
        startBallPosition = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>();
        jellyLayer = LayerMask.NameToLayer(BreadType.JellyBread.ToString());
        pbLayer = LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString());
        startBallLayer = gameObject.layer;
        InitializeOriginalBallDirection();
        ballDirection = originalBallDirection;
        isJamBall = ballOfPlayerType == PlayerType.Jelly;
        ResetBall();
    }
    
    private void InitializeOriginalBallDirection()
    {
        if (gameObject.layer == jellyLayer)
        {
            originalBallDirection = new Vector2(-1, 0f);
        }
        else if (gameObject.layer == pbLayer)
        {
            originalBallDirection = new Vector2(1, 0f);
        }
    }
    
    private void UpdateGravity()
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag: Constants.PLAYER))
        {
            lastPlayerHit = collision.gameObject.GetComponent<Player>().Stats.PlayerType;
            if (lastPlayerHit != ballOfPlayerType && lastPlayerHit == PlayerType.Jelly)
            {
                UpdateLayer(jellyLayer);
                UpdateGravity();
                animator.SetBool(Constants.IS_JAM_BALL, true);
            }
            else if (lastPlayerHit != ballOfPlayerType && lastPlayerHit== PlayerType.PeanutButter)
            {
                UpdateLayer(pbLayer);
                UpdateGravity();
                animator.SetBool(Constants.IS_JAM_BALL, false);
            }
            else if (lastPlayerHit == ballOfPlayerType && animator.GetBool(Constants.IS_JAM_BALL) != isJamBall)
            {
                animator.SetBool(Constants.IS_JAM_BALL, isJamBall);
            }
        }
    }
    
    void LaunchBall()
    {
        // rb.velocity = ballDirection * speed;
        rb.AddForce(-originalBallDirection * speed);
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
        animator.SetBool(Constants.IS_JAM_BALL, isJamBall);
        rb.velocity = Vector2.zero;
        UpdateLayer(startBallLayer);
        Invoke(nameof(LaunchBall), launchDelay);
    }
    
    private IEnumerator ScoredGoal(string tag)
    {
        yield return new WaitForSeconds(ballOffScreenWaitTime);
        if (tag.Equals(Constants.JAM_GOAL) && gameObject.layer == pbLayer)
        {
            GameManager.Instance.HandleGoalToPlayer(PlayerType.Jelly);
        }
        else if (tag.Equals(Constants.PEANUT_BUTTER_GOAL) && gameObject.layer == jellyLayer)
        {
            GameManager.Instance.HandleGoalToPlayer(PlayerType.PeanutButter);
        }
        ResetBall();
    }
}
