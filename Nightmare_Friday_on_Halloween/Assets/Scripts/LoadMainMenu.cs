using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMainMenu : MonoBehaviour
{
  void Start()
{
    Invoke("LoadGameOverScene", 5f);
}

void LoadGameOverScene()
{
    SceneManager.LoadScene(0);
}

}
