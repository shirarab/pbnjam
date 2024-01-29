using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    
    private PlayerType lastPlayerHit;
    
    private Rigidbody2D rb;
    
    private Vector2 ballPosition;

    void Start()
    {
        ballPosition = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void FixedUpdate()
    {
        // Ensure the ball maintains a constant speed.
        rb.velocity = rb.velocity.normalized * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the ball's velocity upon collision.
        Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = reflection;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlayerHit = collision.gameObject.GetComponent<Player>().Stats.PlayerType;
            var ball = GetComponent<SpriteRenderer>();
            if (lastPlayerHit == PlayerType.Jelly)
            {
                ball.color = Color.red;
                // ball.sprite = jamBallSprite;
                UpdateLayer(LayerMask.NameToLayer(BreadType.JellyBread.ToString()));
            }
            else if (lastPlayerHit == PlayerType.PeanutButter)
            {
                ball.color = Color.yellow;
                // ball.sprite = peanutButterBallSprite;
                UpdateLayer(LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString()));
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
        if (other.gameObject.CompareTag("JamGoal"))
        {
           GameManager.Instance.IncrementScore(PlayerType.PeanutButter);
        }
        else if (other.gameObject.CompareTag("PeanutButterGoal"))
        {
            GameManager.Instance.IncrementScore(PlayerType.Jelly);
        }
        transform.position = ballPosition;
        LaunchBall();
    }
}
