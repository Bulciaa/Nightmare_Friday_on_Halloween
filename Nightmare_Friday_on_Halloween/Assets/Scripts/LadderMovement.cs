using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float climbSpeed = 3f;

    private bool isClimbing = false;
    private GameObject player;
    private Rigidbody2D playerRigidbody;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            isClimbing = true;
            playerRigidbody.gravityScale = 0f; // Disable gravity while climbing
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isClimbing = false;
            if (playerRigidbody != null)
            {
                playerRigidbody.gravityScale = 1f; // Re-enable gravity when not climbing
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f); // Stop vertical movement
            }
            player = null;
            playerRigidbody = null;
        }
    }

    private void Update()
    {
        if (isClimbing && player != null)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            // Climb vertically
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, verticalInput * climbSpeed);

            // Move horizontally (optional, adjust as needed)
            playerRigidbody.velocity = new Vector2(horizontalInput * climbSpeed, playerRigidbody.velocity.y);
        }
    }
}
