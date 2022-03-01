using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements a timer which displays the time remaining in mins and secs.
/// Controls when ScoreCounter creates a new leaderboard entry or resets.
///
/// @author Ian
/// @author referenced https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/j:w
/// </summary>
public class Timer : MonoBehaviour
{
    public EventChannel startTimer;
    public EventChannel stopTimer;
    public EventChannel resetTimer;
    public float durationInMinutes = 0.5f;
    private float _secsRemaining;
    private bool _timerIsRunning = false;
    public Text display;
    private ScoreCounter _sc;
    void Start()
    {
        // Set up text object on UI
        _secsRemaining = durationInMinutes * 60;
        DisplayAsMinSec(_secsRemaining);
        
        // Assign objects
        _sc = FindObjectOfType<Canvas>().GetComponent<ScoreCounter>();
        startTimer.OnChange += StartTimer;
        stopTimer.OnChange += StopTimer;
        resetTimer.OnChange += ResetTimer;
    }

    private void OnDestroy()
    {
        // Unsubscribe so it doesn't cause memory leak
        startTimer.OnChange -= StartTimer;
        stopTimer.OnChange -= StopTimer;
        resetTimer.OnChange -= ResetTimer;
    }
    void Update()
    {
        if (_timerIsRunning)
        {
            if (_secsRemaining > 0) // timer counting down
            {
                // for every frame, subtract the time taken for prev frame
                _secsRemaining -= Time.deltaTime; 
                DisplayAsMinSec(_secsRemaining);
            }
            else // timer ended naturally
            {
                stopTimer.Publish();
            }
        }
        
        if (Input.GetButtonDown("Submit")) 
        {
            ControlTime(); // start, stop or reset time
        }
    }

    private void DisplayAsMinSec(float time)
    {
        float m = Mathf.FloorToInt(time / 60);
        float s = Mathf.FloorToInt(time % 60);
        display.text = string.Format("{0:00}:{1:00}", m, s);
    }
    
    /// <summary>
    /// Starts, stops and resets timer.
    /// 
    /// Called by pressing the bottom button on the right controller. (A, joystick button 0)
    /// </summary>
    public void ControlTime()
    {
        if (_secsRemaining == 0 || _timerIsRunning)
        {
            resetTimer.Publish();
        }
        else
        {
            startTimer.Publish();
        }
    }
    
    private void StartTimer()
    {
        _timerIsRunning = true;
        _sc.StartCountingScore();
    }
    private void StopTimer()
    {
        _timerIsRunning = false;
        _secsRemaining = 0;
        display.text = "00:00";
        _sc.SaveScore();
        _sc.StopCountingScore();
    }
    
    private void ResetTimer()
    {
        _timerIsRunning = false;
        _secsRemaining = durationInMinutes * 60;
        DisplayAsMinSec(_secsRemaining);
        _sc.ResetScore();
        _sc.StopCountingScore();
    }
}
