using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorialWSAD : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public PlayerController playerController;

    public GameObject FindText;

    private bool tutorialActive = true;
    private void Start()
    {
        StartCoroutine(ShowTutorialSequence());

        if (tutorialActive)
        {
            Time.timeScale = 0; 
        }
    }
    IEnumerator ShowTutorialSequence()
    {
        // Poka¿ animacjê "Find"
        FindText.SetActive(true);
        yield return new WaitForSeconds(6f); // Czas trwania animacji "Find"

        // Poka¿ "CanvasTutorial" i wy³¹cz "Find"
        FindText.SetActive(false);
        TutorialCanvas.SetActive(true);
    }
    public void ExitTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
        tutorialActive = false;

        playerController.Start();
        ResumeGame();

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
