using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitToMainMenu();
        }
    }

    void ExitToMainMenu()
    {
        UnityEngine.Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
