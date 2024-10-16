using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeedVertical;
    string directionHorizontal;
    float moveSpeedHorizontal;
    [SerializeField] float minX, maxX, padding;

    [Header("Fire")]
    [SerializeField] EnemyProjectile1 projectilePrefab;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume;

    void Start()
    {
        switch(Random.Range(1, 3))
        {
            case 1: directionHorizontal = "Left"; moveSpeedHorizontal = 6;break;
            case 2: directionHorizontal = "Right"; moveSpeedHorizontal = -6;break;
        }
        StartCoroutine(ChangeDirection());
        StartCoroutine(FireContinously());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Adjust horizontal speed based on the current direction
        switch (directionHorizontal)
        {
            case "Left": moveSpeedHorizontal -= 24f * Time.deltaTime; break;
            case "Right": moveSpeedHorizontal += 24f * Time.deltaTime; break;
        }

        // Clamp the horizontal speed
        moveSpeedHorizontal = Mathf.Clamp(moveSpeedHorizontal, -6f, 6f);
        float deltaX = moveSpeedHorizontal * Time.deltaTime;

        // Calculate new X position
        float newPositionX = transform.position.x + deltaX;

        // If the enemy reaches a border, change direction and reset speed
        if (newPositionX >= 6f)
        {
            newPositionX = 6f; // Restrict position at the right border
            directionHorizontal = "Left"; // Change direction
            moveSpeedHorizontal = 0; // Reset accumulated speed
        }
        else if (newPositionX <= -6f)
        {
            newPositionX = -6f; // Restrict position at the left border
            directionHorizontal = "Right"; // Change direction
            moveSpeedHorizontal = 0; // Reset accumulated speed
        }

        // Vertical movement
        float deltaY = moveSpeedVertical * Time.deltaTime;
        float newPositionY = transform.position.y + deltaY;

        // Update enemy's position
        transform.position = new Vector2(newPositionX, newPositionY);

        // Destroy the enemy if it goes out of bounds vertically
        if (transform.position.y < -12f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(1f);
        switch(directionHorizontal)
        {
            case "Right": directionHorizontal = "Left";break;
            case "Left": directionHorizontal = "Right";break;
        }
        StartCoroutine(ChangeDirection());
    }

    IEnumerator FireContinously()
    {
        yield return new WaitForSeconds(1f);
        var newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.transform.SetParent(FindObjectOfType<Instances>().projectiles);
        AudioSource.PlayClipAtPoint(ShootSFX, Camera.main.transform.position, shootSFXVolume);
        StartCoroutine(FireContinously());
    }
}
