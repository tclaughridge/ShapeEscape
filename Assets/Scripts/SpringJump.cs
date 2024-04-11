using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
    [SerializeField] private float launchForce = 15.0f;
    [SerializeField] private float colliderThreshold = 0.1f; // A small threshold to account for precision issues

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the spring is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the collision is above the spring
            foreach (ContactPoint2D point in collision.contacts)
            {
                // Check if the y position of the collision point is greater than the y position of the spring's top edge
                if (point.point.y > transform.position.y + (GetComponent<Collider2D>().bounds.size.y / 2) - colliderThreshold)
                {
                    Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

                    // If the player's Rigidbody2D is found and the player is falling downwards (negative y velocity)
                    if (playerRigidbody != null && playerRigidbody.velocity.y <= 0)
                    {
                        // Launch the player up
                        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, launchForce);
                        break; // Exit the loop after applying force
                    }
                }
            }
        }
    }
}
