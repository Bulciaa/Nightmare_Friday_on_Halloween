using UnityEngine;

public class PauseMenu : MonoBehaviour
{
  [SerializeField]  public GameObject pauseMenuUI;
<<<<<<< Updated upstream
    public GameObject gameplayUI;
=======
    [SerializeField]public GameObject gameplayUI;


    private bool isPaused = false;

	void Start()
	{
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
}
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
