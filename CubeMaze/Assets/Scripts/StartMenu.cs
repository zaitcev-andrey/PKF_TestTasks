using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMainMenu;

    private void Start()
    {
        StartMainMenu.SetActive(true);
    }

    public void Button_StartGame()
    {
        SceneManager.LoadScene("Game_CubeMaze");
        Time.timeScale = 1f;
    }

    public void Button_Exit()
    {
        Application.Quit();
    }
}
