using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int healthMax;
    [SerializeField] int health;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] int damage;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume;

    [Header("Score")]
    [SerializeField] int Score;

    public void ProcessHit(int damage)
    {
        /// <summary>
        /// if the health is 0, then destroy this object
        /// Create an Normal explosion's instantiate 
        /// </summary>
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        /// <summary>
        /// Handles the player's movement along the x and y axes based on input.
        /// Movement is constrained to the defined boundaries (minX, maxX, minY, maxY).
        /// </summary>

        FindObjectOfType<GameManager>().AddToScore(Score);
        var newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        newExplosion.transform.SetParent(FindObjectOfType<Instances>().explosions);
        Destroy(gameObject);

        //Set up the audio for enemy destruction
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D either)
    {
         if(either.GetComponent<PlayerShip>())
        {
            either.GetComponent<PlayerShip>().ProcessHit(damage);
            Die();
        }
    }
}
