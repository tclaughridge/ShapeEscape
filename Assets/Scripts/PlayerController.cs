using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float playerSpeed = 4.0f;
    [SerializeField] private float jumpPower = 18.5f;

    private bool canJump = true;
    private bool isPushing = false;

    private Rigidbody2D _playerRigidbody;
    private bool _isFacingRight = true;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();

        // Trigger Jump animation
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            Jump();
            animator.SetTrigger("Jumping");
        }

        // Check if the player is falling
        if (_playerRigidbody.velocity.y < -0.8)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

        if (_playerRigidbody.velocity.x < 0 && _isFacingRight)
        {
            Flip();
        }
        else if (_playerRigidbody.velocity.x > 0 && !_isFacingRight)
        {
            Flip();
        }

        // Trigger Push animation based on bool value
        animator.SetBool("Pushing", isPushing);

        if (_playerRigidbody.position.y < -14)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);

        animator.SetFloat("Speed", Math.Abs(horizontalInput));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destructible"))
        {
            CheckForPushing(collision);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Destructible"))
        {
            canJump = true;
            animator.SetBool("Falling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destructible"))
        {
            isPushing = false;
        }
    }

    private void CheckForPushing(Collision2D collision)
    {
        float horizontalDifference = transform.position.x - collision.transform.position.x;
        float pushDirection = _isFacingRight ? 1.0f : -1.0f;

        if (Input.GetAxisRaw("Horizontal") == pushDirection && Mathf.Sign(horizontalDifference) != pushDirection)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (Mathf.Abs(point.normal.y) < 0.5)
                {
                    isPushing = true;
                    return;
                }
            }
        }
        
        isPushing = false;
    }


    private void Jump()
    {
        if (canJump && Math.Abs(_playerRigidbody.velocity.y) < 0.8)
        {
            _playerRigidbody.velocity = new Vector2(0, jumpPower);
            canJump = false;
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}