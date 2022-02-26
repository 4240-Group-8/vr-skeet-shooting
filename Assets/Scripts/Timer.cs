using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements a timer which displays the time remaining in mins and secs.
/// TODO: figure out how to structure events so that the completion auto-adds the score to leaderboard
/// and resets it, while stopping the timer just resets the score. 
///
/// @author Ian
/// @author referenced https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/j:w
/// </summary>
public class Timer : MonoBehaviour
{
    public float durationInMinutes = 5;
    private float _secsRemaining;
    public bool timerIsRunning = false;
    public Text display;
    void Start()
    {
        _secsRemaining = durationInMinutes * 60;
        ConvertToMinSec(_secsRemaining);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (_secsRemaining > 0)
            {
                // for every frame, subtract the time taken for prev frame
                _secsRemaining -= Time.deltaTime; 
                ConvertToMinSec(_secsRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                _secsRemaining = 0;
                display.text = "0:00";
                timerIsRunning = false;
            }
        }
        if (Input.GetButtonDown("Submit")) // A, joystick button 0, on R ctrller
        {
            ControlTime();
        }
    }

    private void ConvertToMinSec(float time)
    {
        float m = Mathf.FloorToInt(time / 60);
        float s = Mathf.FloorToInt(time % 60);
        display.text = string.Format("{0:00}:{1:00}", m, s);
    }
    
    /// <summary>
    /// Stops, resets, and restarts the timer to control play sessions.
    /// Based on whether the timer has run out or is still running.
    /// 
    /// To be called by a UnityEvent linked to the bottom button on the right controller (A, joystick button 0)
    /// </summary>
    public void ControlTime()
    {
        if (_secsRemaining == 0) // reset timer
        {
            _secsRemaining = durationInMinutes * 60;
            ConvertToMinSec(_secsRemaining);
        }
        else if (timerIsRunning) // stop and reset timer
        {
            timerIsRunning = false;
            _secsRemaining = durationInMinutes * 60;
            ConvertToMinSec(_secsRemaining);
        }
        else // start timer
        {
            timerIsRunning = true;
        }
    }
}
