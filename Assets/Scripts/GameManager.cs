using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI timerText;
    public Button restartButton;
    public GameObject player;
    public GameObject timer;


    public List<GameObject> targetPrefabs;


    private float spawnRate = 1.5f;
   
    public bool isGameActive;

    private float spawnRangeY = -11;

    private float spawnRangeX = 8f;

    private float midIntervalRangeX = 6.52f;

    private float midSpawnPositionX;

    private float spawnPositionX;

    private float spawnRangeZ = -0.07f;





    private float timeLeft;





    public void StartGame(int difficulty)
    {
        timer.SetActive(true);
        timeLeft = 30;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());


    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }

        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
         float[] xIntervals=new float[5];
    midSpawnPositionX = Random.Range(-midIntervalRangeX,midIntervalRangeX);
        xIntervals[0] = -spawnRangeX;
        xIntervals[1] = midSpawnPositionX;
        xIntervals[2] = midSpawnPositionX;
        xIntervals[3] = midSpawnPositionX;
        xIntervals[4] =  spawnRangeX;

        spawnPositionX = xIntervals[Random.Range(0, xIntervals.Length)];
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnRangeY, spawnRangeZ);
        return spawnPosition;

    }










    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        if (timeLeft < 0)
        {
            ;
        }
        else
        {
            ;
        }

        restartButton.gameObject.SetActive(true);


        isGameActive = false;

    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Fall()
    {
        player.GetComponent<SphereCollider>().enabled = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, -17, 0);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        CountDownTimer();
    }
    
    public void CountDownTimer()
    {
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeLeft);
            if (timeLeft < 0)
            {
                GameOver();
                Fall();
            }
        }
    }



    public void Exit()
    {
        Application.Quit();
    }
    void PauseGame()
    {
        if (isGameActive)
        {
            Time.timeScale = 0;
        }
    }




    public void ResumeGame()
    {
        if (isGameActive)
        {
            Time.timeScale = 1;
        }

    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}