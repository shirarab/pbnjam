using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Transform timerKnobTransform;
    private readonly float rotationDegrees = 360;

    private float gameTime;
    private bool stopTimer = true;
    private float currentTime = 0; 

    private void Awake()
    {
        gameTime = GameManager.Instance.gameTime;
    }
    
    private void Update()
    {
        if (stopTimer) return;
        
        currentTime += Time.deltaTime / gameTime;
        var normalizedTime = currentTime > 1f ? 1f : currentTime % 1f;
        
        timerKnobTransform.eulerAngles = new Vector3(0, 0, -normalizedTime * rotationDegrees);
    }

    public void StartTimer()
    {
        stopTimer = false;
    }

    public void StopTimer()
    {
        stopTimer = true;
    }

    public void AddSecondsToTimer(float seconds)
    {
        if (seconds > 0)
        {
            currentTime -= seconds / gameTime;
        }
    }
}
