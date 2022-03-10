using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    public EventChannel pointScored;
    public EventChannel stopTimer;
    public EventChannel resetTimer;
    public EventChannel timeSlowed; // pigeon enters hoop
    public EventChannel gunUnequipped; // pigeon hits ground
    public Text scoreCounter;
    public Text leaderboardVal;
    public int score = 0;
    private int _highscore = 0;
    private bool _isCountingScore;
    private static readonly string ZERO = "0"; // C#'s readonly == Java's final
    void Start()
    {
        scoreCounter.text = ZERO;
        leaderboardVal.text = ZERO;
        pointScored.OnChange += AddPoint;
        timeSlowed.OnChange += StartCountingScore; // when pigeon enters ring
        stopTimer.OnChange += SaveScore;
        resetTimer.OnChange += ResetScore;
        gunUnequipped.OnChange += StopCountingScore;
    }

    private void OnDestroy()
    {
        pointScored.OnChange -= AddPoint;
        timeSlowed.OnChange -= StartCountingScore;
        stopTimer.OnChange -= SaveScore;
        resetTimer.OnChange -= ResetScore;
        gunUnequipped.OnChange -= StopCountingScore;
    }

    public void StartCountingScore()
    {
        _isCountingScore = true;
    }
    
    /// <summary>
    /// Disables the counting of score.
    /// Does not count score when timer is not running.
    /// </summary>
    public void StopCountingScore()
    {
        _isCountingScore = false;
    }
    
    public void AddPoint()
    {
        if (_isCountingScore)
        {
            score++;
            scoreCounter.text = score + "";
        }
    }
    /// <summary>
    /// Adds score to leaderboard after timer is done.
    /// </summary>
    public void SaveScore()
    {
        if (score > _highscore)
        {
            _highscore = score;
            leaderboardVal.text = _highscore + "";
        }
    }
    
    /// <summary>
    /// Resets score on hand when timer is reset.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        scoreCounter.text = score + "";
    }
}
