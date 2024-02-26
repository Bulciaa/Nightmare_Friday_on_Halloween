using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class TreeCutting : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public GameObject buttonA;
    public GameObject buttonD;
    public AIScript opponentScript; // Referencja do skryptu przeciwnika
    public TMP_Text loseText;
    public TMP_Text winText;
    public GameObject karmelekGracza;

    private int score = 0;
    private bool lastKeyPressWasA = false;
    public float gameTime = 30f; // Czas gry w sekundach
    private bool gameRunning = true;

    private Vector3 originalSizeA;
    private Vector3 originalSizeD;
    void Start()
    {
        Time.timeScale = 0;
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

                // Sprawdzenie warunku przegranej po zakoñczeniu czasu
                if (score < opponentScript.GetScore()) // Jeœli wynik gracza jest mniejszy ni¿ przeciwnika
                {
                    StartCoroutine(ShowLoseTextAfterDelay(1f)); // Wywo³aj metodê po 2 sekundach opóŸnienia
                }

                if (score > opponentScript.GetScore()) // Jeœli wynik gracza jest wiêkszy ni¿ przeciwnika
                {
                    StartCoroutine(ShowWinTextAfterDelay(1f)); // Wywo³aj metodê po 2 sekundach opóŸnienia
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

        SpawnCandy();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimeText()
    {
        timeText.text = "Time: " + Mathf.Round(gameTime).ToString();
    }

    IEnumerator ShowLoseTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        loseText.gameObject.SetActive(true); // Wyœwietl tekst "You lose" na ekranie gracza
        yield return new WaitForSeconds(3f); // Odczekaj 3 sekundy

        // Przekieruj gracza do sceny GameOver
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator ShowWinTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winText.gameObject.SetActive(true); // Wyœwietl tekst "You win" na ekranie gracza
        yield return new WaitForSeconds(3f); // Odczekaj 3 sekundy

        // Przekieruj gracza do sceny MenuG³ówne
        SceneManager.LoadScene("MenuG³ówne");
    }
    void SpawnCandy()
    {
        // Instancjonuj prefab karmelka w pozycji startowej
        GameObject candy = Instantiate(karmelekGracza, Vector3.zero, Quaternion.identity);

    }
}
