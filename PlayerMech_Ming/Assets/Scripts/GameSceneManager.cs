using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadPlayerMechGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
