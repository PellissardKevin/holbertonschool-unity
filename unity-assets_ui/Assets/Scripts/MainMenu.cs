using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int level)
    {
        // Load the corresponding level scene based on the parameter
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level01");
                break;
            case 2:
                SceneManager.LoadScene("Level02");
                break;
            case 3:
                SceneManager.LoadScene("Level03");
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
        Application.Quit();
    }
}
