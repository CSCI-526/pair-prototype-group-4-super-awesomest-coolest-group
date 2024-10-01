using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SceneRotation sceneRotation;
    public float speed = 5f;
    public float jumpForceLandscape = 10f; 
    public float fallSpeedLandscape = 0.02f; 
    public float jetpackForce = 8f; 
    public float normalFallSpeed = 0.05f; 
    public float speedModifier = 1f; 
    private Rigidbody2D rb;
    private bool usingJetpack;
    public CameraMovement cameraMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move Left/Right
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed * speedModifier, rb.velocity.y);

        // Check the scene orientation 
        if (!sceneRotation.isVertical)
        {
            // Enable jumping and disable jetpack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForceLandscape); 
            }

            // Slow falling in landscape mode
            if (rb.velocity.y < 0)
            {
                rb.velocity += new Vector2(0, -fallSpeedLandscape);
            }

            usingJetpack = false; 
        }
        else
        {
            // Enable jetpack and disable jumping
            if (Input.GetKey(KeyCode.UpArrow))
            {
                usingJetpack = true;
                rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
            }
            else
            {
                usingJetpack = false;
            }

            // Use normal falling speed in vertical mode
            if (!usingJetpack && rb.velocity.y < 0)
            {
                rb.velocity += new Vector2(0, -normalFallSpeed); // Normal falling speed
            }
        }
    }

    // Handle Collision with Spike
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die(); // Call the Die method if the player touches a spike
        }
    }

    // Trigger detection for the Finish Line
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WinTrigger"))
        {
            Win();
            cameraMovement.StopCamera();
            sceneRotation.StopRotation();
        }
    }

    // Death method
    void Die()
    {
        Debug.Log("Player has died!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Win method
    void Win()
    {
        Debug.Log("You win!");
    }
}
