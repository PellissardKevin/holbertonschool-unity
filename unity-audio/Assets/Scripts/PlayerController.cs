using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotationSpeed = 10f;
    public Animator animator;
    private float jumpForce = 11f;
    private float groundCheckDistance = 1.2f;
    private Vector3 respawnOffset = new Vector3(0, 15, 0); // Offset for respawn position
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 startPosition = new Vector3(0, 1, 0);
    public Transform cameraTransform; // Reference to the camera transform
    public bool isJumping;

    public AudioSource grassFootsteps;
    public AudioSource rockFootsteps;
    public LayerMask grassLayer;
    public LayerMask rockLayer;
    public AudioClip footstepSoundGrass;
    public AudioClip footstepSoundRock;

    public AudioSource grassLanding;
    public AudioSource rockLanding;
    public AudioClip landingSoundGrass;
    public AudioClip landingSoundRock;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentSceneName);

        // Load or set initial position
        if (PlayerPrefs.HasKey("PlayerPositionX") && PlayerPrefs.HasKey("PlayerPositionY") && PlayerPrefs.HasKey("PlayerPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            // Set to a default position if no PlayerPrefs are found
            transform.position = startPosition;
        }

        lastPosition = transform.position; // Initialize lastPosition
        isJumping = false;

        PlayerPrefs.Save();
    }

    void Update()
    {
        animator.SetBool("isMoving", false);
        HandleMovement();
        HandleJump();
        CheckRespawn();

        bool isRunning = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isRunning && isGrounded && !isJumping)
        {
            PlayFootsteps();
        }
        else
        {
            StopFootsteps();
        }
    }

    private Vector3 lastPosition;

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

        // Move the player
        rb.MovePosition(newPosition);

        // Rotate the player to face the movement direction
        if (desiredMoveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Save the player's position to PlayerPrefs if it has changed significantly
        if (Vector3.Distance(newPosition, lastPosition) > 0.1f) // You can adjust the threshold
        {
            PlayerPrefs.SetFloat("PlayerPositionX", newPosition.x);
            PlayerPrefs.SetFloat("PlayerPositionY", newPosition.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", newPosition.z);
            PlayerPrefs.Save();
            lastPosition = newPosition;
        }
    }


    private void HandleJump()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance);

        if (isGrounded && hit.collider.CompareTag("Ground") && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            isJumping = true;
            isGrounded = false;
            StopFootsteps();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("isFalling", false);
            PlayLandingSound();
        }
    }

    private void CheckRespawn()
    {
        if (transform.position.y < -10f)
        {
            animator.SetBool("isFalling", true);
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = startPosition + respawnOffset; // Reset player position with offset
        rb.velocity = Vector3.zero; // Reset velocity to prevent continued falling

        animator.SetBool("IsJumping", false);

        // Clear PlayerPrefs related to position
        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PlayerPositionZ");
        PlayerPrefs.Save();
    }


    void PlayFootsteps()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, grassLayer))
        {
            if (!grassFootsteps.isPlaying)
            {
                grassFootsteps.PlayOneShot(footstepSoundGrass);
                rockFootsteps.Stop();
            }
        }
        else if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, rockLayer))
        {
            if (!rockFootsteps.isPlaying)
            {
                rockFootsteps.PlayOneShot(footstepSoundRock);
                grassFootsteps.Stop();
            }
        }
    }

    void StopFootsteps()
    {
        grassFootsteps.Stop();
        rockFootsteps.Stop();
    }

    void PlayLandingSound()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, grassLayer))
        {
            grassLanding.PlayOneShot(landingSoundGrass);
        }
        else if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, rockLayer))
        {
            rockLanding.PlayOneShot(landingSoundRock);
        }
    }
}
