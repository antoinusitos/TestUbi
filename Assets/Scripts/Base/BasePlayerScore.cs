using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayerScore : MonoBehaviour 
{
    public Text scoreText;

    private int _score = 0;

    public void AddScore(int Amount)
    {
        _score += Amount;
        if (scoreText)
            scoreText.text = "score : " + _score;
    }

    public void ResetScore()
    {
        _score = 0;
        if (scoreText)
            scoreText.text = "score : " + _score;
    }
	
}
