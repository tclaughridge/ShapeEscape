using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJump : MonoBehaviour
{
    [SerializeField] private float launchForce = 20.0f;
    private Animator springAnimator;
    private Animator playerAnimator;

    void Start()
    {
        // Get the Animator component from the GameObject
        springAnimator = GetComponent<Animator>();

        // Get the Animator component from the player GameObject
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null && rb.velocity.y <= 0)
        {
            // Trigger the spring contracting animation
            springAnimator.SetTrigger("Contract");
            playerAnimator.SetTrigger("Jumping");

            // Apply an upward force to the player
            Vector2 velocity = rb.velocity;
            velocity.y = launchForce;
            rb.velocity = velocity;
        }
    }
}

