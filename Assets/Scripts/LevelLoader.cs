using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private CanvasManager canvasManager;
    private GameManager gameManager;
    [SerializeField] private float transitionTime = 1f;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadSceneAfterTransition(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        canvasManager.SwitchCanvas(CanvasType.LevelScreen);
        gameManager.StartGame(1);
        SceneManager.LoadScene(levelIndex);
    }
     public void LoadEndingAfterTransition(int sceneIndex)
     {
         StartCoroutine(LoadEnding(sceneIndex));
     }

     IEnumerator LoadEnding(int levelIndex)
     {
         transition.SetTrigger("Start");
         yield return new WaitForSeconds(transitionTime);
         canvasManager.SwitchCanvas(CanvasType.EndScreen);
         SceneManager.LoadScene(levelIndex);
     }
}