using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIPlayer : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    private float increaseInterval = 0.111f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            score++;
            UpdateScoreText();
            timer = 0f;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}



