using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DefaultNamespace;

public class MainMenuScript : MonoBehaviour
{
    private const int MAIN_MENU_SCENE = 0; 
    private const int TUTORIAL_SCENE = 1; 
    private const int GAME_SCENE = 2; 
    private const int GAME_OVER_SCENE = 3; 
    private const int WIN_SCENE = 4; 
    public static void StartGame()
    {
        SpaceshipManager.SetIsStartingTrue();
        SceneManager.LoadScene(TUTORIAL_SCENE);
    }
    public static void StartMainGame() 
    {
        SpaceshipManager.SetIsStartingTrue();
        SceneManager.LoadScene(GAME_SCENE);
    }

    public static void ToMainMenu() 
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public static void ToGameOver() 
    {
        SceneManager.LoadScene(GAME_OVER_SCENE);
    }

    public static void ToWinScreen() 
    {
        SceneManager.LoadScene(WIN_SCENE);
    }

    public static void QuitGame() 
    {
        Application.Quit(); 
    }
}
