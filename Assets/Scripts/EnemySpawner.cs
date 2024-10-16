using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Arrays to store enemy prefabs and waypoint paths for certain enemies
    [SerializeField] GameObject[] enemyPrefab, waypointsPrefab;

    // Delay before the first enemies start spawning
    [SerializeField] float delayTime;

    // Variables to track the wave index and the number of enemies per wave
    int waveIndex, enemyCounter;

    // Time between enemy spawns in a wave
    float spawnTime;

    void Start()
    {
        // Initialize waveIndex to 1 and start the coroutine to spawn enemies
        waveIndex = 1;
        StartCoroutine(StarEnemyCoroutine());
    }

    // Coroutine to delay the start of enemy spawning
    IEnumerator StarEnemyCoroutine()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the initial delay
        StartCoroutine(SpawnEnemies1()); // Start spawning enemy type 1
        StartCoroutine(SpawnEnemies2()); // Start spawning enemy type 2
        StartCoroutine(SpawnWaves());    // Start spawning waves of enemies
    }

    // Method to create and place an enemy in the scene
    void CreateEnemy(GameObject enemy, float positionX, int waypoints = 0)
    {
        // Set the spawn position for the enemy
        Vector2 enemyPosition = new Vector2(positionX, 11f);
        // Instantiate the enemy prefab at the given position
        var newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
        // Assign the enemy to the appropriate parent object (e.g., for organizing in the hierarchy)
        newEnemy.transform.SetParent(FindObjectOfType<Instances>().enemies.transform);

        // Check if the enemy is of type Enemy3, which follows waypoints
        if (newEnemy.GetComponent<Enemy3>())
        {
            // Assign a path (waypoints) based on the input parameter
            switch (waypoints)
            {
                case 1: newEnemy.GetComponent<Enemy3>().SetPath(waypointsPrefab[0]); break;
                case 2: newEnemy.GetComponent<Enemy3>().SetPath(waypointsPrefab[1]); break;
                case 3: newEnemy.GetComponent<Enemy3>().SetPath(waypointsPrefab[2]); break;
                case 4: newEnemy.GetComponent<Enemy3>().SetPath(waypointsPrefab[3]); break;
            }
        }
    }

    // Coroutine to spawn enemies of type 1 at random intervals
    IEnumerator SpawnEnemies1()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f)); // Wait for a random time between 1 and 4 seconds
        CreateEnemy(enemyPrefab[0], Random.Range(-5f, 6f));   // Spawn enemy 1 at a random X position
        StartCoroutine(SpawnEnemies1()); // Repeat the spawning
    }

    // Coroutine to spawn enemies of type 2 at random intervals
    IEnumerator SpawnEnemies2()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f)); // Wait for a random time between 1 and 4 seconds
        CreateEnemy(enemyPrefab[1], Random.Range(-5f, 6f));   // Spawn enemy 2 at a random X position
        StartCoroutine(SpawnEnemies2()); // Repeat the spawning
    }

    // Coroutine to spawn waves of enemies with varying patterns
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds before starting the next wave

        // Set enemy count and spawn time based on the current wave index
        switch (waveIndex)
        {
            case 1: enemyCounter = 3; spawnTime = .17f; break; // 3 enemies with 0.17 seconds between them
            case 2: enemyCounter = 5; spawnTime = .5f; break;  // 5 enemies with 0.5 seconds between them
            case 3: enemyCounter = 3; spawnTime = .17f; break;
            case 4: enemyCounter = 5; spawnTime = .5f; break;
            case 5: enemyCounter = 4; spawnTime = .25f; break;
            case 6: enemyCounter = 7; spawnTime = .3f; break;
            case 7: enemyCounter = 5; spawnTime = .30f; break;
        }

        // Start spawning enemies in the current wave
        StartCoroutine(SapwnWavesEnemy());
        // Continue to the next wave after finishing this one
        StartCoroutine(SpawnWaves());
    }

    // Coroutine to spawn individual enemies for each wave
    IEnumerator SapwnWavesEnemy()
    {
        yield return new WaitForSeconds(spawnTime); // Wait based on the spawn time for this wave

        // Define the X position for spawning, and spawn enemies with different patterns based on the wave index
        float positionX = 0;
        switch (waveIndex)
        {
            case 1: positionX = -4; CreateEnemy(enemyPrefab[2], positionX, 1); break; // Spawn enemyPrefab[2] with waypoints
            case 2: positionX = Random.Range(-5f, 5f); CreateEnemy(enemyPrefab[3], positionX, 1); break; // Spawn enemyPrefab[3]
            case 3: positionX = 4; CreateEnemy(enemyPrefab[2], positionX, 2); break;
            case 4: positionX = Random.Range(-5f, 5f); CreateEnemy(enemyPrefab[3], positionX, 1); break;
            case 5: positionX = -4; CreateEnemy(enemyPrefab[2], positionX, 3); break;
            case 6: positionX = Random.Range(-5f, 5f); CreateEnemy(enemyPrefab[3], positionX, 1); break;
            case 7: positionX = 4; CreateEnemy(enemyPrefab[2], positionX, 4); break;
        }

        enemyCounter--; // Decrease the counter for remaining enemies in this wave

        // Check if all enemies in the current wave have been spawned
        if (enemyCounter == 0)
        {
            waveIndex++; // Move to the next wave
            if (waveIndex > 7) // Reset waveIndex if it exceeds the total number of waves
                waveIndex = 1;
        }
        else
        {
            StartCoroutine(SapwnWavesEnemy()); // Continue spawning remaining enemies in the current wave
        }
    }
}
