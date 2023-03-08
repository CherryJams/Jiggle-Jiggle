using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI timerText;
    public List<GameObject> balloons;
    public CanvasManager canvasManager;
    public List<GameObject> hazards;
    public bool isGameActive;
    private int lives;
    [SerializeField] float spawnRate = 1.5f;
    [SerializeField] float spawnRangeY = -11;
    [SerializeField] float spawnRangeX = 8f;
    [SerializeField] float midIntervalRangeX = 6.52f;
    private float midSpawnPositionX;
    private float spawnPositionX;
    private float spawnRangeZ = -0.07f;
    private float timeLeft;

    public void StartGame(int difficulty)
    {
        timeLeft = 30;
        spawnRate /= difficulty;
        isGameActive = true;
        lives = 2;
        StartCoroutine(SpawnTarget());
    }

    private void AddBalloonsToList()
    {
        Transform balloonsParent = GameObject.FindGameObjectWithTag("Balloons").transform;
        foreach (GameObject balloonChild in balloonsParent)
        {
            balloons.Add(balloonChild);
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, hazards.Count);

            if (isGameActive)
            {
                Instantiate(hazards[index], RandomSpawnPosition(), hazards[index].transform.rotation);
            }
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float[] xIntervals = new float[5];
        midSpawnPositionX = Random.Range(-midIntervalRangeX, midIntervalRangeX);
        xIntervals[0] = -spawnRangeX;
        xIntervals[1] = midSpawnPositionX;
        xIntervals[2] = midSpawnPositionX;
        xIntervals[3] = midSpawnPositionX;
        xIntervals[4] = spawnRangeX;

        spawnPositionX = xIntervals[Random.Range(0, xIntervals.Length)];
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnRangeY, spawnRangeZ);
        return spawnPosition;
    }


    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(2);
        }

        canvasManager.SwitchCanvas(CanvasType.GameOverScreen);

        isGameActive = false;
    }


    void FlyToWaypoint()
    {
        foreach (GameObject player in balloons)
        {
            player.GetComponent<CapsuleCollider2D>().enabled = false;
            player.GetComponent<Waypoints>().MoveToCurrentWaypoint();
        }
    }

    public void Update()
    {
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
                FlyToWaypoint();
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
    }
}