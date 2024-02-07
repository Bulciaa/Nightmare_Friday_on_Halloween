using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  	[SerializeField]  public GameObject mainMenuUI;
	[SerializeField]  public GameObject settingsUI;

	public void ZmieńSettings()
{
	mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
}

	public void ZmieńMainMenu()
{
	mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
}
}
