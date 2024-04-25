using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Behavior : MonoBehaviour
{
    private Transform target;
    public Rigidbody2D enemy_rb;    // Rigid body of the enemy

    public float Enemy_Speed = 1f;  // Speed of the enemy (needs to be tweaked)
    private float target_xpos;      // variable to store the x position of the player
    private int move_direction = 1; // sets the enemy's move direction towards the player. If -1, it moves left and if 1, it moves right

    private void Awake()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // identify the target
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the target variable
        if (target != null)
        {
            target_xpos = target.position.x;
        }

        // Sets the move direction by comparing the enemy position to the player's
        if (target_xpos < enemy_rb.position.x)
        {
            move_direction = -1;
        } else
        {
            move_direction = 1;
        }

        // Move the enemy towards the player at the speed specified above
        enemy_rb.velocity = new Vector2((Enemy_Speed * move_direction), enemy_rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks to see if the object it collides with is the player
        if (collision.collider.tag == "Player")
        {
            // Destroys the player
            Destroy(collision.collider.gameObject);
        }
    }
}
