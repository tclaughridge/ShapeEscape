using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 5f; // Seconds to explosion
    public float blastRadius = 5f; // Radius of effect
    public LayerMask destructionLayer; // Layer of objects that can be destroyed

    private bool isActivated = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the bomb
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
        Destroy(gameObject); // Destroy the bomb
    }

    private void Explode()
    {
        // Perform explosion logic (affecting objects within blastRadius)
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, blastRadius, destructionLayer);

        foreach (Collider2D obj in objectsInRange)
        {
            // Add explosion force, damage, etc.
            Destroy(obj.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
