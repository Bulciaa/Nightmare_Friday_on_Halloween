using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitTutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public TreeCutting treeCutting; // Referencja do skryptu TreeCuttingGame
    public AIScript aiScript; // Referencja do skryptu TreeCuttingGame



    private void Start()
    {
        PauseGame();
	
    }

    public void ExitTutorialCanvas()
    {
        treeCutting.tutorialActive = false;
        aiScript.tutorialActive = false;
        TutorialCanvas.SetActive(false);
        ResumeGame();
        
    }

    public void PauseGame()
    {
        treeCutting.tutorialActive = true;
        aiScript.tutorialActive = true;
        // Dezaktywuj skrypt lub obiekt z logik¹ gry
        treeCutting.enabled = false;
        aiScript.enabled = false;
    }

    public void ResumeGame()
    {   
        // Aktywuj skrypt lub obiekt z logik¹ gry
        treeCutting.enabled = true;
        aiScript.enabled = true;


        Debug.Log("Game resumed");
    }

	
}

