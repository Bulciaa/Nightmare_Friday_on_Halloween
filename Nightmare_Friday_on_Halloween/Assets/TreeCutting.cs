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
    public GameObject buttonA;
    public GameObject buttonD;

    private int score = 0;
    private bool lastKeyPressWasA = false;
    private float gameTime = 10f; // Czas gry w sekundach
    private bool gameRunning = true;

    private Vector3 originalSizeA;
    private Vector3 originalSizeD;
    void Start()
    {
        originalSizeA = buttonA.transform.localScale;
        originalSizeD = buttonD.transform.localScale;

        // Ustawienie kwadracika A wiêkszego ni¿ D na pocz¹tku gry
        buttonA.transform.localScale = originalSizeA * 1.2f;


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

                // ZnajdŸ obiekt AIScript i zatrzymaj naliczanie punktów
                AIScript aiScript = FindObjectOfType<AIScript>();
                if (aiScript != null)
                {
                    aiScript.StopScoreCounting();
                }
            }

            UpdateTimeText();

            if (Input.GetKeyDown(KeyCode.A) && !lastKeyPressWasA)
            {
                DecreaseSize(buttonA);
                IncreaseSize(buttonD);
                IncreaseScore();
                lastKeyPressWasA = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastKeyPressWasA)
            {
                DecreaseSize(buttonD);
                IncreaseSize(buttonA);
                IncreaseScore();
                lastKeyPressWasA = false;
            }
        }
    }
    void IncreaseSize(GameObject button)
    {
        button.transform.localScale = originalSizeA * 1.2f;
    }

    void DecreaseSize(GameObject button)
    {
        button.transform.localScale = originalSizeA * 0.8f;
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
