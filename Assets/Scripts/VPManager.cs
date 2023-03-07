using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VPManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer kidRunningVP;
    [SerializeField] private VideoPlayer kidFallingVP;
    [SerializeField] private CanvasManager canvasManager;
    

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
        canvasManager.SwitchCanvas(CanvasType.LevelScreen);
        SceneManager.LoadScene(1);
    }

}