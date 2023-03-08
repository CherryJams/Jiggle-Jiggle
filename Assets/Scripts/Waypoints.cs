using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject player;
    int current = 0;
    public float speed;
    float WPradius = 1;


    public void MoveToCurrentWaypoint()
    {
        if (IsDistanceBetweenObjectAndCurrentWaypointLessThanWPRadius())
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

        transform.position =
            Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    private bool IsDistanceBetweenObjectAndCurrentWaypointLessThanWPRadius()
    {
        return Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius;
    }
}