using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    public float turnSpeed = 4.0f;
    public bool isInverted = false; // Public variable to control Y axis inversion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;

        // Set the cameraTransform in the PlayerController
        player.GetComponent<PlayerController>().cameraTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PauseMenu.isPaused)
        {
            return; // Skip the rest of the update if paused
        }

        float mouseY = Input.GetAxis("Mouse Y");
        if (isInverted)
        {
            mouseY *= -1f; // Invert the Y axis input if isInverted is true
        }

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(mouseY * turnSpeed, Vector3.left) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }

    public void ResetOffset()
    {
        offset = transform.position - player.transform.position; // Recalculate the offset
    }
}

