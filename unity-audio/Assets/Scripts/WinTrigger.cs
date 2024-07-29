using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text winText; // Reference to the Text component for displaying win message
    public GameObject winCanvas;
    public AudioClip victoryPiano; // Reference to the victory sound clip
    private AudioSource audioSource;
    private bool hasWon = false;
    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
    }

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

            // Stop the background music
            if (musicManager != null)
            {
                musicManager.StopMusic();
            }

            // Play the victory sound
            if (audioSource != null && victoryPiano != null)
            {
                audioSource.clip = victoryPiano;
                audioSource.Play();
            }
        }
    }
}
