using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using Unity.Services.Analytics;

public class ZarządzanieScenami : MonoBehaviour
{
public void PlaySound()
{
	  GetComponent<AudioSource>().Play();
	
}

public void WyjdzMainMenu()
{
	SceneManager.LoadScene(0);
}

 public void ZmienScene()
{
	SceneManager.LoadScene(1);
	AnalyticsResult analyticsResultOrb = Analytics.CustomEvent("StartGame");
		 Debug.Log("Gracz rozpoczął grę " + analyticsResultOrb);
	
	
}

public void Settings()
{
	SceneManager.LoadScene(1);
}

public void PominCutscene()
{
	StartCoroutine(Pomijanie());
	
	
}

private IEnumerator Pomijanie()
    {
     yield return new WaitForSeconds(0.8f);
	SceneManager.LoadScene(2);
      
    }

public void Wyjdz()
{
AnalyticsResult analyticsResultOrb = Analytics.CustomEvent("ExitGame");
		 Debug.Log("Gracz wyszedł z gry " + analyticsResultOrb);
Application.Quit();
}
}
