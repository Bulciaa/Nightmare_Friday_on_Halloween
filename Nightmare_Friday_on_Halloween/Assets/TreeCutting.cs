using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class TreeCutting : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timeText;
    private int score = 0;
    private bool lastKeyPressWasA = false;
    private float gameTime = 45f; // Czas gry w sekundach
    private bool gameRunning = true;

    void Start()
    {
        UpdateScoreText();
        UpdateTimeText();
    }

    void Update()
    {
        if (gameRunning)
        {
            gameTime -= Time.deltaTime;

            if (gameTime <= 0)
            {
                gameTime = 0;
                gameRunning = false;
            }

            UpdateTimeText();

            if (Input.GetKeyDown(KeyCode.A) && !lastKeyPressWasA)
            {
                IncreaseScore();
                lastKeyPressWasA = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastKeyPressWasA)
            {
                IncreaseScore();
                lastKeyPressWasA = false;
            }
        }
    }

    void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimeText()
    {
        timeText.text = "Time: " + Mathf.Round(gameTime).ToString();
    }
}
