using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] float gameSpeed; // Controls the overall speed of the game (time scale)
    [SerializeField] PlayerShip playerShipPrefab; // Reference to the player ship prefab for respawning
    [SerializeField] int lives; // Number of lives the player has

    GameHUD gameHud; // Reference to the GameHUD script for updating UI
    int currentScore; // Player's current score

    private void Start()
    {
        gameHud = FindObjectOfType<GameHUD>(); // Find the GameHUD in the scene
        gameHud.UpdateLives(lives); // Initialize and update the UI with the player's lives
        gameHud.UpdateScore(currentScore); // Initialize and update the UI with the player's current score
    }

    void Update()
    {
        Time.timeScale = gameSpeed; // Adjust the game's time scale based on the gameSpeed value
    }

    // Get the current score
    public int GetScore()
    {
        return currentScore;
    }

    // Add points to the player's score and update the UI
    public void AddToScore(int points)
    {
        currentScore += points; // Increment the current score
        //Debug.Log(currentScore); // (Optional) Debug log to show the score in the console
        gameHud.UpdateScore(currentScore); // Update the score in the HUD
    }

    // Handle the player's death and respawn logic
    public void ProcessDeath()
    {
        if (lives > 0) // If the player still has lives remaining
        {
            lives--; // Decrease the player's lives by one
            gameHud.UpdateLives(lives); // Update the UI with the remaining lives
            gameHud.UpdateHealthBar(1, 1); // Reset the health bar to full
            Instantiate(playerShipPrefab, new Vector2(0f, -12f), Quaternion.identity); // Respawn the player ship at a given position
        }
        else
        {
            //Game Over logic can be implemented here when the player runs out of lives
        }
    }
}
