using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 255);
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = new Color32(255, 0, 0, 255);
            goalMat.color = new Color32(0, 255, 0, 255);
        }
        
        SceneManager.LoadScene("maze");
    }
    public void QuitMaze()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
