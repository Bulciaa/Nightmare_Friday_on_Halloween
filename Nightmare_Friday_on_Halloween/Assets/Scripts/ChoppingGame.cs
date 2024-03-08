using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ChoppingGame : MonoBehaviour
{
    public TMP_Text playerScoreText;
    public TMP_Text opponentScoreText;
    public TMP_Text timerText;
    public float gameDuration = 45f;
    public float opponentScoreInterval = 0.3f;

    private float gameTimeLeft;
    private int playerScore;
    private int opponentScore;
    private bool gameStarted;

    void Start()
    {
        gameTimeLeft = gameDuration;
        UpdateUI();
    }

    void Update()
    {
        if (gameStarted)
        {
            gameTimeLeft -= Time.deltaTime;

            if (gameTimeLeft <= 0)
            {
                gameTimeLeft = 0;
                gameStarted = false;
            }

            UpdateTimer();

            if (Input.GetKeyDown(KeyCode.A))
            {
                IncrementPlayerScore();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                IncrementPlayerScore();
            }

            if (gameTimeLeft > 0)
            {
                opponentScore += Mathf.FloorToInt(Time.deltaTime / opponentScoreInterval);
            }

            UpdateUI();
        }
    }

    void IncrementPlayerScore()
    {
        if (Input.inputString.Equals("a") && playerScore % 2 == 0)
        {
            playerScore++;
        }
        else if (Input.inputString.Equals("d") && playerScore % 2 == 1)
        {
            playerScore++;
        }
    }

    void UpdateTimer()
    {
        timerText.text = "Time Left: " + Mathf.Ceil(gameTimeLeft).ToString();
    }

    void UpdateUI()
    {
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        opponentScoreText.text = "Opponent Score: " + opponentScore.ToString();
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}