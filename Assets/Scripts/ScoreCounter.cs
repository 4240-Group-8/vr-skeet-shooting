using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreCounter;
    public int score = 0;

    /*
    IEnumerator AddPoint()
    {
        if (listen for event)
        {
            score++;
            // consider moving the udpate here also
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = score + ""; // convert to string
    }
}
