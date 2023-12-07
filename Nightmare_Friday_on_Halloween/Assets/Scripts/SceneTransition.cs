using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += LoadNextScene;
    }

    private void LoadNextScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(2);
    }
}
