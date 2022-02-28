using UnityEngine;
using UnityEngine.Events;
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
    public float durationInMinutes = 0.5f;
    private float _secsRemaining;
    private bool _timerIsRunning = false;
    public Text display;
    private ScoreCounter _sc;
    private UnityEvent _startTimer = new UnityEvent(); // can make public later on
    private UnityEvent _stopTimer = new UnityEvent(); // can make public later on
    private UnityEvent _resetTimer = new UnityEvent(); // can make public later on
    void Start()
    {
        _secsRemaining = durationInMinutes * 60;
        DisplayAsMinSec(_secsRemaining);
        
        _sc = FindObjectOfType<Canvas>().GetComponent<ScoreCounter>();
        
        _startTimer.AddListener(delegate 
        {
            _timerIsRunning = true;
            _sc.StartCountingScore();
        });
        
        _stopTimer.AddListener(delegate 
        {
            _timerIsRunning = false;
            _secsRemaining = 0;
            display.text = "00:00";
            _sc.SaveScore();
            _sc.StopCountingScore();
        });
        
        _resetTimer.AddListener(delegate 
        {
            _timerIsRunning = false;
            _secsRemaining = durationInMinutes * 60;
            DisplayAsMinSec(_secsRemaining);
            _sc.ResetScore();
            _sc.StopCountingScore();
        });
        
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
                _stopTimer.Invoke();
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
            _resetTimer.Invoke();
        }
        else
        {
            _startTimer.Invoke();
        }
    }
}
