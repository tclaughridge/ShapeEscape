using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Behavior : MonoBehaviour
{

    // This has not been tested yet but I plan to test this in a bit

    public GameObject player;   // Player game object
    public Rigidbody2D enemy_rb;    // Rigid body of the enemy

    public float Enemy_Speed = 1f;  // Speed of the enemy (needs to be tweaked)
    private float target_xpos;      // variable to store the x position of the player
    private int move_direction = 1; // sets the enemy's move direction towards the player. If -1, it moves left and if 1, it moves right


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the target variable
        target_xpos = player.transform.position.x;

        // Sets the move direction by comparing the enemy position to the player's
        if (target_xpos < enemy_rb.position.x)
        {
            move_direction = -1;
        } else
        {
            move_direction = 1;
        }

        // Move the enemy towards the player at the speed specified above
        enemy_rb.position = new Vector2(enemy_rb.position.x + (Enemy_Speed * move_direction), enemy_rb.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //  (TODO) Game over / Restart level
    }
}
