using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform cameraTransform; // Reference to the camera transform
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Default");
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentSceneName);

        // Load the saved position if it exists
        if (PlayerPrefs.HasKey("PlayerPositionX") && PlayerPrefs.HasKey("PlayerPositionY") && PlayerPrefs.HasKey("PlayerPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            transform.position = new Vector3(x, y, z);
        }

        startPosition = transform.position;
        PlayerPrefs.Save();
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

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveVertical + right * moveHorizontal;
        Vector3 movement = desiredMoveDirection * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + movement;

        rb.MovePosition(newPosition);

        // Save the player's position to PlayerPrefs
        PlayerPrefs.SetFloat("PlayerPositionX", newPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", newPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", newPosition.z);
        PlayerPrefs.Save();
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
