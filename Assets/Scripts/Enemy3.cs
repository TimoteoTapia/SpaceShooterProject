using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeed; // Speed at which the enemy moves
    // List of waypoints that the enemy will follow
    List<Transform> waypointList;

    int currentWaypoint; // Tracks the current waypoint, initialized to 0 by default

    void Update()
    {
        Move(); // Continuously move towards the next waypoint
    }

    // Method to move the enemy towards the current waypoint
    private void Move()
    {
        // Check if the waypoint list is set, if not log an error and stop movement
        if (waypointList == null)
        {
            Debug.LogError("Waypoint list is null! Make sure to set the path before moving.");
            return; // Exit the method if waypointList is null
        }

        // Check if the enemy has more waypoints to move towards
        if (currentWaypoint < waypointList.Count)
        {
            var targetPosition = waypointList[currentWaypoint].position; // Get the position of the current waypoint
            float delta = moveSpeed * Time.deltaTime; // Calculate how far to move this frame based on speed
            // Move the enemy towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            // If the enemy reaches the target position, move to the next waypoint
            if (transform.position == targetPosition)
                currentWaypoint++; // Increment to the next waypoint
        }
        else
            Destroy(gameObject); // Destroy the enemy object when all waypoints are reached
    }

    // Method to set the path for the enemy by passing a GameObject that contains waypoints as children
    public void SetPath(GameObject waypointObject)
    {
        waypointList = new List<Transform>(); // Initialize the list of waypoints
        // Loop through each child of the waypointObject and add it to the list
        foreach (Transform child in waypointObject.transform)
        {
            waypointList.Add(child);
        }
    }
}
