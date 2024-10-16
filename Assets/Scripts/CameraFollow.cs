using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player; // Reference to the player (PlayerShip)
    [SerializeField] float smoothSpeed = 0.125f; // Camera follow smoothness
    [SerializeField] Vector3 offset; // Offset distance between the camera and the player
    // Define the camera's horizontal limits
    [SerializeField] float cameraMinX, cameraMaxX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FollowPlayer();
    }

    void FollowPlayer()
    {
        // Calculate the desired position (player position with offset)
        Vector3 desiredPosition = player.position + offset;

        // Clamp the camera's position to prevent it from moving beyond the set limits
        float clampedX = Mathf.Clamp(desiredPosition.x, cameraMinX, cameraMaxX);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, transform.position.y, transform.position.z), smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;
    }
}
