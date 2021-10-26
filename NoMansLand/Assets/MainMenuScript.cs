using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame() 
    {
        Debug.Log("Now playing game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Scene Loaded");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
