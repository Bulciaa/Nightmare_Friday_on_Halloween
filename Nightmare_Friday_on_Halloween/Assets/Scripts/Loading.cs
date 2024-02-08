using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
      public string nextSceneName;
	

    void Start()
    {
	Invoke("LoadingScene", 3f);   
    }

  	void LoadingScene()
    {
	
        SceneManager.LoadScene(nextSceneName);
    }
}
