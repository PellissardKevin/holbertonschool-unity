using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void Back()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        Debug.Log(currentSceneName);

        // Load the current scene again
        SceneManager.LoadScene(currentSceneName);
    }
}
