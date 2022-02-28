using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    public Text scoreCounter;
    public Canvas leaderboard;
    public GameObject leaderboardItem; // prefab
    public GameObject newLeaderboardItem; // instantiated
    public int score = 0;
    private bool _isCountingScore = false;
    void Start()
    {
        scoreCounter.text = score + "";
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
            scoreCounter.text = score + ""; // convert to string
        }
    }
    /// <summary>
    /// Adds score to leaderboard after timer is done.
    /// </summary>
    public void SaveScore()
    {
        // add a new ui element to leaderboard containing the score
        /*
        newLeaderboardItem = Instantiate(leaderboardItem, leaderboard.transform);
        newLeaderboardItem.GetComponentInChildren<Text>().text = score + "";
        */
        // how do i know which text object it changes?
        Debug.Log("Your new highscore is " + score);
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
