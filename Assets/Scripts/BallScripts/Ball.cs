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
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerType lastPlayerHit;
    private Vector2 startBallPosition;
    private Sprite startBallSprite;
    private int jellyLayer;
    private int pbLayer;
    private Sprite newBallSprite;

    private void Awake()
    {
        startBallPosition = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startBallSprite = newBallSprite = spriteRenderer.sprite;
        jellyLayer = LayerMask.NameToLayer(BreadType.JellyBread.ToString());
        pbLayer = LayerMask.NameToLayer(BreadType.PeanutButterBread.ToString());
        LaunchBall();
    }
    
    void FixedUpdate()
    {
        // Ensure the ball maintains a constant speed.
        rb.velocity = rb.velocity.normalized * (speed * Time.deltaTime);
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
            if (lastPlayerHit == PlayerType.Jelly)
            {
                UpdateLayer(jellyLayer);
                // untested yet
                newBallSprite = jamBallSprite;
            }
            else if (lastPlayerHit == PlayerType.PeanutButter)
            {
                UpdateLayer(pbLayer);
                // untested yet
                newBallSprite = peanutButterBallSprite;
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
