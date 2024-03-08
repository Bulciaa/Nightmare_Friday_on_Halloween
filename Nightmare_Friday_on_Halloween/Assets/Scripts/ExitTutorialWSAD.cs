using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorialWSAD : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public PlayerController playerController;
    public GameObject textFind;
    public GameObject tutorialScreen;

    private bool tutorialActive = true;

    private void Start()
    {

        StartCoroutine(ShowTextAnimation());
    }

    private IEnumerator ShowTextAnimation()
    {

        tutorialScreen.SetActive(false);
        textFind.SetActive(true);
        yield return new WaitForSeconds(6f);
        textFind.SetActive(false);
        tutorialScreen.SetActive(true);
        tutorialActive = true; // Set tutorialActive to true after showing the second object

        if (!tutorialActive)
        {
            Time.timeScale = 0;
        }
    }

    public void ExitTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
        tutorialActive = false;

        playerController.enabled = true; // Enable player controller

        ResumeGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
