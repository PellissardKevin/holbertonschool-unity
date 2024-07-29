using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    public Slider bgmSlider;  // Reference to the BGM volume slider
    public Slider sfxSlider;  // Reference to the SFX volume slider
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

        // Load and set BGM volume slider value
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);  // Default volume is 1
        bgmSlider.value = savedBGMVolume;

        // Load and set SFX volume slider value
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);  // Default volume is 1
        sfxSlider.value = savedSFXVolume;

        // Add listeners for slider value changes
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    public void Apply()
    {
        // Save invert Y setting
        PlayerPrefs.SetInt("IsInverted", invertYToggle.isOn ? 1 : 0);

        // Save BGM volume setting
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);

        // Save SFX volume setting
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

        PlayerPrefs.Save();

        // Update cameraController.isInverted with invertYToggle.isOn value
        if (cameraController != null)
        {
            cameraController.isInverted = invertYToggle.isOn;
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

        // Ensure MusicManager is not in the paused state
        if (musicManager != null)
        {
            musicManager.EnsureDefaultSnapshot(); // Ensure it transitions to the default snapshot
        }

        string previousSceneName = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(previousSceneName);
    }

    private void OnBGMVolumeChanged(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetBGMVolume(volume);  // Set volume on the MusicManager
        }
        else
        {
            Debug.LogWarning("MusicManager not found in the scene.");
        }
    }

    private void OnSFXVolumeChanged(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetSFXVolume(volume);  // Set volume on the MusicManager
        }
        else
        {
            Debug.LogWarning("MusicManager not found in the scene.");
        }
    }
}
