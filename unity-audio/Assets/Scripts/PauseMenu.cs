using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    public static bool isPaused = false;

    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        if (musicManager == null)
        {
            Debug.LogError("MusicManager not found in the scene.");
        }
    }

    void Update()
    {
        // Check if the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // If the game is paused, resume the game
                Resume();
            }
            else
            {
                // If the game is not paused, pause the game
                Pause();
            }
        }
    }


    public void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        // Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Activate the pause canvas
        PauseCanvas.SetActive(true);

        // Transition to paused snapshot
        if (musicManager != null)
        {
            musicManager.PauseGame();
        }

    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Deactivate the pause canvas
        PauseCanvas.SetActive(false);

        // Transition to default snapshot
        if (musicManager != null)
        {
            musicManager.ResumeGame();
        }
    }

    public void Restart()
    {
        // Reset all PlayerPrefs
        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PlayerPositionZ");
        PlayerPrefs.Save(); // Ensure changes are saved

        // Reset isPaused and Time.timeScale before reloading the scene
        isPaused = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseCanvas.SetActive(false);
    }

    public void MainMenu()
    {
        // Reset isPaused and Time.timeScale before loading the MainMenu scene
        isPaused = false;
        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PlayerPositionZ");
        Time.timeScale = 1f;

        // Transition to default snapshot
        if (musicManager != null)
        {
            musicManager.ResumeGame();
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        // Reset isPaused and Time.timeScale before loading the Options scene
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
    }

    public void UnpauseFromPauseMenu()
    {
        // Unpause the game from the PauseMenu
        Resume();
    }
}
