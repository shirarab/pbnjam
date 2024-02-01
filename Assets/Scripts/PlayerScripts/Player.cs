using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    
    #region PLAYER FIELDS ------------------------------
    [SerializeField] 
    PlayerComponents components;


    [SerializeField]
    InputAction moveAction;
    public PlayerComponents Components { get => components;}


    [SerializeField] 
    PlayerStats stats;
    public PlayerStats Stats { get => stats;}


    [SerializeField] 
    private PlayerAnimator playerAnimator; 
    
    [SerializeField]
    private float maxBounceAngle = 45f;
    public PlayerAnimator PlayerAnimator { get => this.playerAnimator; set => this.playerAnimator = value; }
    #endregion



    #region METHOD FLAGS AND HELPERS ------------------
    [SerializeField] 
    float durationOfHitAnimation; //duration of the hit animation

    private Vector2 _moveInput;
    #endregion


    
    #region ANIMATION ------------------------------------
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // Check if the colliding GameObject has a specific tag.
        if (collision.gameObject.CompareTag(Constants.BALL))
        {
            Vector3 playerPosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;
            
            float offset = playerPosition.y - contactPoint.y;
            float width = collision.otherCollider.bounds.size.y;
            Ball ball = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            float currentAngle = Vector2.SignedAngle(ball.getBallDirection(), ballRb.velocity);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle =Mathf.Clamp( bounceAngle + currentAngle, -maxBounceAngle, maxBounceAngle);
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ballRb.velocity = rotation * ball.getBallDirection() * ballRb.velocity.magnitude;
            
            PlayerAnimator.PlayAnimaion(AnimationType.Hit);
            StartCoroutine(playerAnimator.BackToIdle(durationOfHitAnimation));
        }
        
        else if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    #endregion




    #region MOVEMENT--------------------------------
    private void MovePlayer()
    {
        _moveInput = moveAction.ReadValue<Vector2>();
        Vector2 force = new Vector2(_moveInput.x * Stats.StartSpeed, _moveInput.y * Stats.StartSpeed);
        Components.RigidBody.AddForce(force);
    }
    
    private void stopMotion()
    {
        if (Input.GetKeyUp(Stats.KeyUp))
        {
            Components.RigidBody.velocity = Vector2.zero;   
        }

        if (Input.GetKeyUp(Stats.KeyDown))
        {
            Components.RigidBody.velocity = Vector2.zero;   
        }
    }
    #endregion

    #region ADDED EVENT SYSTEM----------------------------------------------

    private void playEndOfGameAnimation(int winner)
    {
        Debug.Log("ENTER - playEndOfGameAnimation");
        if((int)stats.PlayerType == winner)
        {
            PlayerAnimator.TriggerAnimation(AnimationType.Win);
        }
        else
        {
            PlayerAnimator.TriggerAnimation(AnimationType.Lose);
        }
    }

    private void OnDisable()
    {
        EventManager.GameOverAnimation -= playEndOfGameAnimation;
    }
    #endregion


    #region START, UPDATE --------------------------------
    
    void Start()
    {
        moveAction.Enable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        stopMotion();
    }
    #endregion
}
