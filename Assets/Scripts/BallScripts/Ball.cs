using System;
using System.Collections;
using UnityEngine;
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
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerType lastPlayerHit;
    private Vector2 startBallPosition;
    private Sprite startBallSprite;

    private void Awake()
    {
        startBallPosition = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startBallSprite = spriteRenderer.sprite;
        LaunchBall();
    }
    
    void FixedUpdate()
    {
        // Ensure the ball maintains a constant speed.
        rb.velocity = rb.velocity.normalized * (speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlayerHit = collision.gameObject.GetComponent<Player>().Stats.PlayerType;
            if (lastPlayerHit == PlayerType.Jelly)
            {
                UpdateLayer(LayerMask.NameToLayer(BreadType.JellyBread.ToString()));
                // untested yet
                spriteRenderer.sprite = jamBallSprite;
            }
            else if (lastPlayerHit == PlayerType.PeanutButter)
            {
                UpdateLayer(LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString()));
                // untested yet
                spriteRenderer.sprite = peanutButterBallSprite;
            }
        }
    }
    
    void LaunchBall()
    {
        // Launch the ball in a random direction.
        float randomAngle = Random.Range(0, 360f);
        Vector2 launchDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        rb.velocity = launchDirection * speed;
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
        spriteRenderer.sprite = startBallSprite;
        LaunchBall();
    }
    
    private IEnumerator ScoredGoal(string tag)
    {
        Debug.Log("before wait");
        yield return new WaitForSeconds(ballOffScreenWaitTime);
        Debug.Log(tag + " scored a goal!");
        if (tag.Equals("JamGoal"))
        {
            GameManager.Instance.DecrementScore(PlayerType.Jelly);
        }
        else if (tag.Equals("PeanutButterGoal"))
        {
            GameManager.Instance.DecrementScore(PlayerType.PeanutButter);
        }
        ResetBall();
    }
}
