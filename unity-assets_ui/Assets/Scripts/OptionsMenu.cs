using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    private CameraController cameraController;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();

        // Check if cameraController is found
        if (cameraController != null)
        {
            invertYToggle.isOn = PlayerPrefs.GetInt("IsInverted", 0) == 1;
        }
    }

    public void Apply()
    {
        // Save the invert Y setting to PlayerPrefs
        PlayerPrefs.SetInt("IsInverted", invertYToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        // Update cameraController.isInverted with invertYToggle.isOn value
        if (cameraController != null)
        {
            cameraController.isInverted = invertYToggle.isOn;
            Debug.Log("cameraController: " + cameraController.isInverted);
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
}
