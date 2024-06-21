using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text winText; // Reference to the Text component for displaying win message
    public GameObject winCanvas;
    private bool hasWon = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            Timer timer = FindObjectOfType<Timer>();
            // Stop the timer
            timer.StopTimer();

            winCanvas.SetActive(true);

            // Show the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Set the camera controller flag to stop the camera
            CameraController.isWinMenuActive = true;

            if (timer != null)
            {
                timer.Win();
                hasWon = true;
            }
        }
    }
}
