using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    [SerializeField] private float playerSpeed = 7.0f;
    [SerializeField] private float jumpPower = 7.5f;

    public bool canJump = true;

    private Rigidbody2D _playerRigidbody;
    private bool _isFacingRight = true;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (_playerRigidbody.velocity.x < 0 && _isFacingRight)
        {
            Flip();
        }
        else if (_playerRigidbody.velocity.x > 0 && !_isFacingRight)
        {
            Flip();
        }

        // animator.SetFloat("Speed", Math.Abs(_playerRigidbody.velocity.x));
        // animator.SetBool("IsJumping", !canJump);
        // animator.SetBool("IsAttacking", _isAttacking);

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     swordCollider.enabled = true;
        //     _isAttacking = true;
            
        //     StartCoroutine(ResetAttack(attackSpeed));
        // }
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = true;
        }
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

    // IEnumerator ResetAttack(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     swordCollider.enabled = false;
    //     _isAttacking = false;
    // }
}
