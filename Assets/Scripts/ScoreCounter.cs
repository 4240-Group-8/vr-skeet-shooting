using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    public Text scoreCounter;
    public int score = 0;
    void Start()
    {
        scoreCounter.text = score + "";
    }
    public void AddPoint()
    {
        score++;
        scoreCounter.text = score + ""; // convert to string
    }
}
