using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorialWSAD : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public PlayerController playerController;

    private bool tutorialActive = true;
    private void Start()
    {
        if (tutorialActive)
        {
            Time.timeScale = 0; 
        }
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
