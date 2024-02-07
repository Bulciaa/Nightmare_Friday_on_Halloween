using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]  public GameObject pauseMenuUI;
    [SerializeField]public GameObject gameplayUI;
	

	public Animator anim;

    private bool isPaused = false;

	void Start()
	{
		 anim = GetComponent<Animator>();
		 ResumeGame();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
	
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
	
    }

	public void ExitGame()
	{
		ResumeGame();
		SceneManager.LoadScene(0);
	}
}	
