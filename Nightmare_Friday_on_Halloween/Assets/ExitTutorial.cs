using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorial : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public void ExitTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
    }
}
