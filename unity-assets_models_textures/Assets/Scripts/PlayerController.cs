using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 11f;
    private float groundCheckDistance = 1.2f;
    private LayerMask groundLayer;
    private float fallThreshold = -5f; // Threshold below which the player will respawn
    private Vector3 respawnOffset = new Vector3(0, 15, 0); // Offset for respawn position

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        groundLayer = LayerMask.GetMask("Default");
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        CheckRespawn();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    private void HandleJump()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name); // Add this line
                                                                   // Check if the player has landed on the ground to reset isGrounded
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void CheckRespawn()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = startPosition + respawnOffset; // Reset player position with offset
        rb.velocity = Vector3.zero; // Reset velocity to prevent continued falling
    }
}
