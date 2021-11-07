using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Woman : MonoBehaviour
{
    public GameObject WomanDialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    public GameObject AlertPoint;

    int counter;
    string[] lines = new string[] {
        "Девушка: Эй.. Помогите!",
        "Вы: Что случилось?",
        "Девушка: Там - в конце дороги не так давно появилось что-то странное",
        "Вы: О чем вы?",
        "Девушка: Непонятное сооружение, очень мрачное..",
        "Девушка: А еще оно издает какие-то звуки...",
        "Девушка: Пожалуйста, проверьте что это такое!"};

    public static bool WomanDialogOpen = false;
    public static bool WomanDialogComplete = false;

    bool isOnTriggerStay = false;

    void Start()
    {
        text = TextMeshPro.GetComponent<TMPro.TMP_Text>();
        counter = 0;
    }

    void Update()
    {
        if(isOnTriggerStay)
        {
            if(GUIController.QuestNumber == 4)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        Destroy(AlertPoint, 0f);
                        WomanDialogOpen = false;
                        WomanDialog.SetActive(false);
                        WomanDialogComplete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 4)
        {
            if(!WomanDialogComplete)
            {
                WomanDialogOpen = true;
                WomanDialog.SetActive(true);
                counter = 0;
                text.text = lines[counter];
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        isOnTriggerStay = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        isOnTriggerStay = false;
        WomanDialogOpen = false;
        WomanDialog.SetActive(false);
    }
}
