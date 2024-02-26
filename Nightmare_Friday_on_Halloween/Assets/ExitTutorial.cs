using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitTutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
	

	public void Start()
	{
		
		Time.timeScale = 0;
	}

    public void ExitTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
    }

	public void ResumeGame()
	{
	
		Time.timeScale = 1;

	}
	
	

}
