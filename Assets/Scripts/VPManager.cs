using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VPManager : Singleton<VPManager>
{
    [SerializeField] private VideoPlayer kidRunningVP;
    [SerializeField] private VideoPlayer kidFallingVP;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private GameManager gameManager;

    public void PlayUntilEnd()
    {
        kidRunningVP.loopPointReached += ChangeVideo;
    }

    private void ChangeVideo(VideoPlayer source)
    {
        kidRunningVP.gameObject.SetActive(false);
        kidFallingVP.Play();
        kidFallingVP.loopPointReached += ChangeScene;
        
    }

    private void ChangeScene(VideoPlayer source)
    {
        SceneManager.LoadScene(1);
        canvasManager.SwitchCanvas(CanvasType.LevelScreen);
        gameManager.StartGame(1);
    }

}