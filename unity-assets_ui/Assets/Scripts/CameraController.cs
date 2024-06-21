using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    public float turnSpeed = 4.0f;
    public bool isInverted = false;
    public static bool isWinMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        isInverted = PlayerPrefs.GetInt("IsInverted", 0) == 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;

        // Set the cameraTransform in the PlayerController
        player.GetComponent<PlayerController>().cameraTransform = transform;

        // Reset isWinMenuActive when a new level starts
        isWinMenuActive = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PauseMenu.isPaused || isWinMenuActive)
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

