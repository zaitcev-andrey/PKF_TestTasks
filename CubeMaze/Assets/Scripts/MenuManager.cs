using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isMenuPaused = false;

    public bool isMenuSettings = false;
    public GameObject MenuPaused;
    public GameObject MenuSettings;
    [SerializeField] KeyCode KeyMenuPaused;

    private void Start()
    {
        MenuPaused.SetActive(false);
        MenuSettings.SetActive(false);
    }

    private void Update()
    {
        ActiveMenu();
    }

    private void ActiveMenu()
    {
        if(Input.GetKeyDown(KeyMenuPaused))
        {
            if(isMenuSettings)
            {
                isMenuSettings = false;
                MenuSettings.SetActive(false);
                MenuPaused.SetActive(true);
            }
            else
            {
                isMenuPaused = !isMenuPaused;
                if (isMenuPaused)
                {
                    MenuPaused.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    // останавливаем время в сцене
                    Time.timeScale = 0f;
                }
                else
                {
                    MenuPaused.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    // возобновляем время в сцене
                    Time.timeScale = 1f;
                }
            }        
        }
    }

    public void Button_MenuPausedContinue()
    {
        isMenuPaused = false;
        MenuPaused.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void Button_MenuPausedRestart()
    {
        SceneManager.LoadScene("Game_CubeMaze");
        Time.timeScale = 1f;
    }

    public void Button_MenuPausedSettings()
    {
        isMenuSettings = true;
        MenuPaused.SetActive(false);
        MenuSettings.SetActive(true);
    }

    public void Button_GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Button_MenuPausedExit()
    {
        Application.Quit();
    }
}
