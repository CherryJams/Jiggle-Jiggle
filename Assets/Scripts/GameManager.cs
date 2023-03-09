using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI timerText;
    public BalloonDeath[] balloons;
    public CanvasManager canvasManager;
    public List<GameObject> hazards;
    public GameObject[] onScreenHazards;
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
        DestroyOnScreenHazards();
        ResetBalloonsPosition();
        timeLeft = 2;
        spawnRate /= difficulty;
        isGameActive = true;
        lives = 2;
        StartCoroutine(SpawnTarget());
    }

    private void DestroyOnScreenHazards()
    {
        onScreenHazards = GameObject.FindGameObjectsWithTag("Hazard");
        foreach (GameObject onScreenHazard in onScreenHazards)
        {
            Destroy(onScreenHazard);
        }
    }

    private void ResetBalloonsPosition()
    {
        balloons = FindObjectsOfType<BalloonDeath>();
        foreach (BalloonDeath balloon in balloons)
        {
            balloon.Reset();
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
        canvasManager.SwitchCanvas(CanvasType.GameOverScreen);

        isGameActive = false;
    }

    private void Victory()
    {
            SceneManager.LoadScene(2);
    }


    void FlyToWaypoint()
    {
        GameObject[] balloons =GameObject.FindGameObjectsWithTag("Balloon");
        Waypoints[] waypoints = FindObjectsOfType<Waypoints>();
        foreach (GameObject balloon in balloons)
        {
            balloon.GetComponent<CapsuleCollider2D>().enabled = false;
        }

        foreach (Waypoints waypoint in waypoints)
        {
           waypoint.MoveToCurrentWaypoint(); 
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
                isGameActive = false;
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