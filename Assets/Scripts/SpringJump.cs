using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
    [SerializeField] private float launchForce = 20.0f;
    private Animator springAnimator;

    void Start()
    {
        // Get the Animator component from the GameObject
        springAnimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f) // Checks if the player is moving downwards
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Trigger the spring contracting animation
                springAnimator.SetTrigger("Contract");

                // Apply an upward force to the player
                Vector2 velocity = rb.velocity;
                velocity.y = launchForce;
                rb.velocity = velocity;
            }
        }
    }
}
