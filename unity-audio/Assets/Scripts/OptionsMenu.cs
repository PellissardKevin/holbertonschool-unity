using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    public Slider bgmSlider;  // Reference to the BGM volume slider
    private CameraController cameraController;
    private MusicManager musicManager;  // Reference to the MusicManager

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        musicManager = FindObjectOfType<MusicManager>();  // Find MusicManager in the scene

        // Load saved invert Y setting
        if (cameraController != null)
        {
            invertYToggle.isOn = PlayerPrefs.GetInt("IsInverted", 0) == 1;
        }

        // Load saved BGM volume setting and set slider value
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);  // Default volume is 1
        bgmSlider.value = savedVolume;

        // Add listener for slider value change
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    public void Apply()
    {
        // Save the invert Y setting to PlayerPrefs
        PlayerPrefs.SetInt("IsInverted", invertYToggle.isOn ? 1 : 0);

        // Save the BGM volume setting to PlayerPrefs
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);

        PlayerPrefs.Save();

        // Update cameraController.isInverted with invertYToggle.isOn value
        if (cameraController != null)
        {
            cameraController.isInverted = invertYToggle.isOn;
            Debug.Log("cameraController: " + cameraController.isInverted);
        }

        // Ensure MusicManager is not in the paused state
        if (musicManager != null)
        {
            musicManager.EnsureDefaultSnapshot(); // Ensure it transitions to the default snapshot
        }

        Back();
    }

    public void Back()
    {
        // Assuming the player GameObject has the tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            // Save the player's position to PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);
        }

        string previousSceneName = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(previousSceneName);
    }

    private void OnBGMVolumeChanged(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);  // Set volume on the MusicManager
        }
        else
        {
            Debug.LogWarning("MusicManager not found in the scene.");
        }
    }
}
