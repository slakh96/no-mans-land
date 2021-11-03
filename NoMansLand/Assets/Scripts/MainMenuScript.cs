using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuScript : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Application.Quit(); 
    }
}
