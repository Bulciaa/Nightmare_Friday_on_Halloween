using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wyjdź : MonoBehaviour
{

public void ExitMainMenu()
{
	SceneManager.LoadScene(0);
}

public void Wyjdz()
{
Application.Quit();
}
}