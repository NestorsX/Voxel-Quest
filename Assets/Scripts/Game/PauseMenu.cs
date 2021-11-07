using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject GUI;
    public GameObject settings;
    
    public static bool GameIsPaused = false;
    
    public static bool isSettingsVisible = false;

    void Update()
    {
        if(!isSettingsVisible)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        UnityEngine.Cursor.visible = false;
        pauseMenu.SetActive(false);
        GUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        UnityEngine.Cursor.visible = true;
        pauseMenu.SetActive(true);
        GUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Settings()
    {
        isSettingsVisible = true;
        settings.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
