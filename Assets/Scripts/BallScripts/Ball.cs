using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    private PlayerType lastPlayerHit;
    
    private Rigidbody2D rb;

    void Start()
    {
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
                UpdateLayer(LayerMask.NameToLayer(BreadType.JellyBread.ToString()));
            }
            else if (lastPlayerHit == PlayerType.PeanutButter)
            {
                ball.color = Color.yellow;
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
}
