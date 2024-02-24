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
    public Animator characterAnimator; // Dodany Animator dla postaci


    private int score = 0;
    private bool lastKeyPressWasA = false;
    private float gameTime = 30f; // Czas gry w sekundach
    private bool gameRunning = true;

    private Vector3 originalSizeA;
    private Vector3 originalSizeD;
    void Start()
    {
        originalSizeA = buttonA.transform.localScale;
        originalSizeD = buttonD.transform.localScale;

        // Ustawienie kwadracika A wi kszego ni  D na pocz tku gry
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

                // Znajd  obiekt AIScript i zatrzymaj naliczanie punkt w
                AIScript aiScript = FindObjectOfType<AIScript>();
                if (aiScript != null)
                {
                    aiScript.StopScoreCounting();
                }

                // Sprawdzenie warunku przegranej po zako czeniu czasu
                if (score < opponentScript.GetScore()) // Je li wynik gracza jest mniejszy ni  przeciwnika
                {
                    StartCoroutine(ShowLoseTextAfterDelay(1f)); // Wywo aj metod  po 2 sekundach op nienia
                }

                if (score > opponentScript.GetScore()) // Je li wynik gracza jest wi kszy ni  przeciwnika
                {
                    StartCoroutine(ShowWinTextAfterDelay(1f)); // Wywo aj metod  po 2 sekundach op nienia
                }
            }

            UpdateTimeText();

            if (Input.GetKeyDown(KeyCode.A) && !lastKeyPressWasA)
            {
                DecreaseSize(buttonA);
                IncreaseSize(buttonD);
                IncreaseScore();
                lastKeyPressWasA = true;
               // PlayCharacterAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastKeyPressWasA)
            {
                DecreaseSize(buttonD);
                IncreaseSize(buttonA);
                IncreaseScore();
                lastKeyPressWasA = false;
               // PlayCharacterAnimation();
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
        loseText.gameObject.SetActive(true); // Wy wietl tekst "You lose" na ekranie gracza
        yield return new WaitForSeconds(3f); // Odczekaj 3 sekundy

        // Przekieruj gracza do sceny GameOver
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator ShowWinTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winText.gameObject.SetActive(true); // Wy wietl tekst "You win" na ekranie gracza
        yield return new WaitForSeconds(3f); // Odczekaj 3 sekundy

        // Przekieruj gracza do sceny MenuG  wne
        SceneManager.LoadScene(0);
    }
    void SpawnCandy()
    {
        // Instancjonuj prefab karmelka w pozycji startowej
        GameObject candy = Instantiate(karmelekGracza, Vector3.zero, Quaternion.identity);

    }
 //   void PlayCharacterAnimation()
  //  {
   //     characterAnimator.SetTrigger("JasonKarmelki"); // "Chop" to nazwa wyzwalacza w kontrolerze animacji
   // }
}
