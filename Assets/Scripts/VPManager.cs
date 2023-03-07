using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VPManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer kidRunningVP;
    [SerializeField] private VideoPlayer kidFallingVP;

    public void PlayUntilEnd()
    {
        Debug.Log("pressed start");
        kidRunningVP.loopPointReached += ChangeVideo;
    }

    private void ChangeVideo(VideoPlayer source)
    {
        kidFallingVP.Play();
        kidFallingVP.loopPointReached += ChangeScene;
    }

    private void ChangeScene(VideoPlayer source)
    {
        SceneManager.LoadScene(1);
    }
     public void DebugCheck()
    {
        Debug.Log("hey its debug time");
    }
}