using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
    [SerializeField] private float launchForce = 15.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the spring is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Check if the player's Rigidbody2D component was found
            if (playerRigidbody != null)
            {
                // Apply an upward force to the player's Rigidbody2D
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, launchForce);
            }
        }
    }
}
