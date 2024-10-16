using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int healthMax; // Maximum health of the player
    [SerializeField] int health; // Current health of the player
    [SerializeField] GameObject explosionPrefab; // Explosion effect prefab when the player dies
    [SerializeField] AudioClip deathSFX; // Sound effect for player death
    [SerializeField] [Range(0, 1)] float deathSFXVolume; // Volume for death sound effect

    [Header("Move")]
    [SerializeField] float moveSpeed; // Speed at which the player ship moves
    [SerializeField] float minX, maxX, minY, maxY, padding; // Movement boundaries for the player
    [SerializeField] GameObject[] propulsionFire; // Visual effect for propulsion fire

    [Header("Fire")]
    [SerializeField] float fireRate; // Rate of fire (time between shots)
    [SerializeField] AudioClip shootSFX; // Sound effect for shooting
    [SerializeField] FireSpark[] fireSparks; // FireSpark objects where projectiles are spawned
    [SerializeField] [Range(0, 1)] float shootSFXVolume; // Volume for shooting sound effect
    [SerializeField] PlayerProjectile projectilePrefab; // Prefab for the player's projectile

    Coroutine fireCoroutine; // To control continuous firing
    GameManager gameManager; // Reference to the GameManager
    bool isReady; // Flag to check if the player is ready for movement and firing
    bool isInvinsible; // Flag for invincibility after spawning

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find and store the GameManager in the scene
        isInvinsible = true; // Player is invincible at the start
        StartCoroutine(RemoveInvicibility()); // Start invincibility removal coroutine
    }

    void Update()
    {
        if (!isReady)
        {
            GetReady(); // Move the player ship to the starting position
        }
        else
        {
            Move(); // Handle player movement
            Fire(); // Handle firing input
            PropulsionControl(); // Handle propulsion fire visual effects
        }
    }

    // Move the player to the starting position at the beginning of the game
    void GetReady()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, -6.5f), 5 * Time.deltaTime);
        if (transform.position.y == -6.5f)
        {
            isReady = true; // Player is ready to move and fire
        }
    }

    // Handle player movement based on input
    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // Calculate horizontal movement
        float deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // Calculate vertical movement
        // Clamp player position within screen boundaries
        float newPosX = Mathf.Clamp(transform.position.x + deltaX, minX + padding, maxX - padding);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, minY + padding, maxY - padding);
        transform.position = new Vector2(newPosX, newPosY); // Apply movement
    }

    // Control the activation of propulsion fire based on player movement
    void PropulsionControl()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .1f || Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            propulsionFire[0].SetActive(true); // Show propulsion fire when moving
            propulsionFire[1].SetActive(true);
        }
        else
        {
            propulsionFire[0].SetActive(false); // Hide propulsion fire when not moving
            propulsionFire[1].SetActive(false);
        }
    }

    // Handle firing input and control continuous firing
    void Fire()
    {
        if (Input.GetButtonDown("Fire1")) // Start firing when the fire button is pressed
        {
            fireCoroutine = StartCoroutine(FireContinously()); // Start continuous firing
        }
        if (Input.GetButtonUp("Fire1")) // Stop firing when the fire button is released
        {
            if (fireCoroutine != null)
                StopCoroutine(fireCoroutine); // Stop continuous firing
        }
    }

    // Instantiate projectiles at the fire spark positions
    private void ShootProjectile(Vector2 projectilePosition)
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume); // Play shoot sound effect
        var newProjectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity); // Create new projectile
        newProjectile.transform.SetParent(FindObjectOfType<Instances>().projectiles.transform); // Set parent for better organization
    }

    // Handle player taking damage
    public void ProcessHit(int damage)
    {
        if (!isInvinsible) // Only take damage if not invincible
        {
            health -= damage; // Reduce player health
            FindObjectOfType<GameHUD>().UpdateHealthBar(healthMax, health); // Update health bar in UI
            if (health <= 0)
                Die(); // Call Die method if health is zero or below
        }
    }

    // Handle player death
    void Die()
    {
        gameManager.ProcessDeath(); // Notify GameManager of player death
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume); // Play death sound effect
        var newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Create explosion effect
        newExplosion.transform.SetParent(FindObjectOfType<Instances>().explosions.transform); // Set parent for organization
        Destroy(gameObject); // Destroy the player object
    }

    // Continuously fire projectiles at regular intervals
    IEnumerator FireContinously()
    {
        fireSparks[0].ShowSpark(); // Show spark effect at fire point 0
        fireSparks[1].ShowSpark(); // Show spark effect at fire point 1
        ShootProjectile(fireSparks[0].transform.position); // Fire a projectile from spark 0
        ShootProjectile(fireSparks[1].transform.position); // Fire a projectile from spark 1
        yield return new WaitForSeconds(fireRate); // Wait for the fire rate duration
        fireCoroutine = StartCoroutine(FireContinously()); // Repeat firing
    }

    // Coroutine to remove invincibility after a delay
    IEnumerator RemoveInvicibility()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        GetComponent<Blinker>().enabled = false; // Disable the blinking effect after invincibility period
        GetComponent<SpriteRenderer>().color = Color.white; // Reset player color to normal
        isInvinsible = false; // Player is no longer invincible
    }
}
