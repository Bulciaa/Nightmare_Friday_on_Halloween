using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PrzejscieDoSceny : MonoBehaviour
{
    public string nextSceneName;
public AudioSource click;

    private void OnMouseDown()
    {
	StartCoroutine(LoadSceneWithDelay());
        
    }
  IEnumerator LoadSceneWithDelay()
    {
	click.Play();
        yield return new WaitForSeconds(0.8f); // Sekunda opóźnienia

        SceneManager.LoadScene(nextSceneName);
    }

}
