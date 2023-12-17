using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class ZarządzanieScenami : MonoBehaviour
{
public void PlaySound()
{
	  GetComponent<AudioSource>().Play();
	
}

 public void ZmienScene()
{
	
	SceneManager.LoadScene(1);
	Analytics.CustomEvent("startGame");
}

public void Wyjdz()
{
Application.Quit();
}
}
