using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile2 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume;
    void Update()
    {
        Vector3 delta = transform.up * moveSpeed * Time.deltaTime;
        transform.position += delta;
        if (transform.position.x < -6f || transform.position.x > 6f || transform.position.y < -12f || transform.position.y > 12f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        var newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        newExplosion.transform.SetParent(FindObjectOfType<Instances>().explosions);
        player.GetComponent<PlayerShip>().ProcessHit(damage);
        Destroy(gameObject);

    }
}
