using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public TreeCutting treeCuttingGame; // Referencja do skryptu zarz�dzaj�cego gr�

    public void ExitTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
    }
}
