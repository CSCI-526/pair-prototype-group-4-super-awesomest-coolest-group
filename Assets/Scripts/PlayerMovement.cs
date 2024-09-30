using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float jetpackForce = 8f;
    public LayerMask groundLayer; // Set this to the tiles layer
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool usingJetpack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move Left/Right
        float moveInput = Input.GetAxis("Horizontal"); // "Horizontal" maps to Left and Right arrows or A/D keys
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Check if the player is on the ground (using ground check for jump)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump if space is pressed and player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Jetpack usage
        if (Input.GetKey(KeyCode.UpArrow))
        {
            usingJetpack = true;
            rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
        }
        else
        {
            usingJetpack = false;
        }

        // Handle falling mechanics
        if (!usingJetpack && !isGrounded)
        {
            rb.velocity += new Vector2(0, -0.05f); // Simulating falling downward
        }
    }


    // Handle Collision with Spike
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die(); // Call the Die method if player touches a spike
        }
    }

    // Death method
    void Die()
    {
        // You can choose what happens here. For now, we'll just reload the scene
        Debug.Log("Player has died!");

        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Debugging: Drawing ground check radius
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
