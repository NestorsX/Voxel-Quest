using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SettingsScreen;
    public GameObject AboutScreen;

    public TMPro.TMP_Text GeneralTimerText;

    int sceneID;
    public float GeneralTimer;

    public static bool isFirstEntry = true;
    public static bool NewGame = false;

    void Start()
    {      
        UnityEngine.Cursor.visible = true;
        if(isFirstEntry)
        {
            GeneralTimer = 0f;
            isFirstEntry = false;
        }
        GeneralTimer = PlayerPrefs.GetFloat("GeneralTimer");
        UpdateGeneralTimer();
    }

    void FixedUpdate()
    {
        GeneralTimer += Time.deltaTime;
        UpdateGeneralTimer();
    }

    void UpdateGeneralTimer()
    {
        int time = (int)GeneralTimer;
        int hours = (int)time / 3600;
        time = time - hours*3600;
        int minutes = (int)time / 60;
        time = time - minutes * 60;
        int seconds = time;
        if(minutes>=60)
            minutes = 0;
        GeneralTimerText.text = $"Время: {hours:00}:{minutes:00}:{seconds:00}";
    }

    public void Play()
    {
        UnityEngine.Cursor.visible = false;
        NewGame = true;
        SceneManager.LoadScene(1);
        PlayerPrefs.SetFloat("GeneralTimer", GeneralTimer);
    }

    public void Continue()
    {
        int sceneID = PlayerPrefs.GetInt("CurrentScene");
        if(sceneID != 0)
        {
            UnityEngine.Cursor.visible = false;
            PlayerPrefs.SetFloat("GeneralTimer", GeneralTimer);
            SceneManager.LoadScene(sceneID);
        }
    }

    public void Settings()
    {
        SettingsScreen.SetActive(true);
        Menu.SetActive(false);
    }

    public void About()
    {
        AboutScreen.SetActive(true);
        Menu.SetActive(false);
    }

    public void Exit()
    {
        PlayerPrefs.SetFloat("GeneralTimer", GeneralTimer);
        Application.Quit();
    }
}
