using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] int damage;
    [SerializeField] GameObject explosionPrefab; // store the explosion prefab

    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set up the movement of the projectile
        float deltaY = moveSpeed * Time.deltaTime;
        float newPosY = transform.position.y + deltaY;
        transform.position = new Vector2(transform.position.x, newPosY);

        //Destroy the projectile if go out of the camera 
        if(transform.position.y > 10.5)
        {
            Destroy(gameObject); 
        }
    }

    /// <summary>
    /// Handles the projectile's collision with the enemies. The parameter is the enemy's object.
    /// As "is trigger" is True, it'll trigger when the projectile has a collision(Enemies)
    /// The function is a built-in property in the system
    /// </summary>
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        //When the projectile have a collision, it'll search the script and function of that object
        enemy.GetComponent<Destroyable>().ProcessHit(damage);
        Destroy(gameObject);// destoy this object with the script(the projectile)

        //Create a copy of the small explosion's prefab
        var newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        newExplosion.transform.SetParent(FindObjectOfType<Instances>().explosions);

        //Set up the audio for enemy hit
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
    }
}
