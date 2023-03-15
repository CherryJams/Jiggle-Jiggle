using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VPManager : Singleton<VPManager>
{
    [SerializeField] private VideoPlayer kidRunningVP;
    [SerializeField] private VideoPlayer kidFallingVP;
    [SerializeField] private VideoPlayer EndingP1VP;
    [SerializeField] private VideoPlayer EndingP2VP;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private AudioManager audioManager;

    public void PlayUntilEnd()
    {
        kidRunningVP.loopPointReached += ChangeVideoToKidFalling;
    }

    private void ChangeVideoToKidFalling(VideoPlayer source)
    {
        kidRunningVP.gameObject.SetActive(false);
        kidFallingVP.Play();
        kidFallingVP.loopPointReached += ChangeScene;
    }

    private void ChangeScene(VideoPlayer source)
    {
        levelLoader.LoadSceneAfterTransition(1);
    }

    public void ChangeVideoToEnding()
    {
        kidFallingVP.gameObject.SetActive(false);
        levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadEndingAfterTransition(2);
        StartCoroutine(waiter());
        EndingP1VP.loopPointReached += ChangeVideoToEndingPart2;
    }

    private void ChangeVideoToEndingPart2(VideoPlayer source)
    {
        EndingP1VP.gameObject.SetActive(false);
        EndingP2VP.Play();
        StartCoroutine(waitAndSayThanks());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
        EndingP1VP.Play();
        audioManager.SwitchToEndingMusic();
    }

    IEnumerator waitAndSayThanks()
    {
        yield return new WaitForSeconds(7f);
        canvasManager.SayThanks();
    }
}