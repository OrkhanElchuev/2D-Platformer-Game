using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSound;
    [SerializeField] int pointsForCoin = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Increment score
        FindObjectOfType<GameSession>().AddToScore(pointsForCoin);
        // Execute coin picking up sound effect
        AudioSource.PlayClipAtPoint(coinPickUpSound, Camera.main.transform.position);
        // When collided with player, destroy itself
        Destroy(gameObject);
    }
}
