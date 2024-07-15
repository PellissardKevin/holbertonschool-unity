using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentSceneName);
    }

    public void LevelSelect(int level)
    {
        // Load the corresponding level scene based on the parameter
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level01");
                // Reset PlayerPrefs
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                break;
            case 2:
                SceneManager.LoadScene("Level02");
                // Reset PlayerPrefs
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                break;
            case 3:
                SceneManager.LoadScene("Level03");
                // Reset PlayerPrefs
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                break;
            default:
                Debug.LogError("Invalid level number!");
                break;
        }
    }

    public void Options()
    {
        // Load the Options scene
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        // Quit the game
        Debug.Log("Exited");
        // Reset PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
}
