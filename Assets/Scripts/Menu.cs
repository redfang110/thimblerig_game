using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void Credits()
    {
        Debug.Log("Credits");
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene(0);
    }
}
