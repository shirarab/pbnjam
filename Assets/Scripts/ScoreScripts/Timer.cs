using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Transform timerKnobTransform;
    private readonly float rotationDegrees = 360;

    private float gameTime;
    private bool stopTimer = true;
    private float currentTime = 0; 

    private bool timerSoundStarted = false;

    private void Awake()
    {
        gameTime = GameManager.Instance.gameTime;
    }
    
    private void Update()
    {
        if (stopTimer) return;
        
        // Debug.Log($"[Timer.Update] currentTime 1: {currentTime}");
        currentTime += Time.deltaTime / gameTime;
        currentTime = currentTime > 1f ? 1f : currentTime;
        // Debug.Log($"[Timer.Update] currentTime 2: {currentTime}");
        var normalizedTime = currentTime % 1f;
        
        timerKnobTransform.eulerAngles = new Vector3(0, 0, -normalizedTime * rotationDegrees);
        if (timerKnobTransform.eulerAngles.z <= 65 && !timerSoundStarted)
        {
            timerSoundStarted = true;
            GameManager.Instance.PlayTimerSound();
        }
    }

    public void StartTimer(bool isRestart = false)
    {
        Debug.Log($"[Timer.StartTimer] isRestart: {isRestart}");
        if (isRestart)
        {
            currentTime = 0f;
        }
        stopTimer = false;
    }

    public void StopTimer()
    {
        Debug.Log($"[Timer.StopTimer]");
        stopTimer = true;
    }

    public void AddSecondsToTimer(float seconds)
    {
        Debug.Log($"[Timer.AddSecondsToTimer]");
        if (seconds > 0)
        {
            currentTime -= seconds / gameTime;
        }
    }
}
