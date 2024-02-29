using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorialWSAD : MonoBehaviour
{
    public GameObject TutorialCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        PauseGame();
    }

    public void ExitTutorialCanvas()
    {

        TutorialCanvas.SetActive(false);
        ResumeGame();

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
