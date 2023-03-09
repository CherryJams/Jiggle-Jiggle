using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject balloon;
    int current = 0;
    [SerializeField] private int speed = 2;
    float WPradius = 1;


    public void MoveToCurrentWaypoint()
    {
        if (IsDistanceBetweenObjectAndCurrentWaypointLessThanWPRadius())
        {
            if (waypoints[current].CompareTag("VictoryWaypoint"))
            {
                SceneManager.LoadScene(2);
            }
       }
        balloon.transform.position =
            Vector3.MoveTowards(balloon.transform.position, waypoints[current].transform.position,
                Time.deltaTime * speed);
    }

    private bool IsDistanceBetweenObjectAndCurrentWaypointLessThanWPRadius()
    {
        return Vector3.Distance(waypoints[current].transform.position, balloon.transform.position) < WPradius;
    }
}