using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest3 : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    int counter;
    string[] lines = new string[] {
        "Вы: Дверь заперта, я не смогу открыть ее просто так..",
        "Вы: Нужно поискать другие способы. Может быть есть какой-то рычаг?",
        "Вы: ...",
        "Вы: Балконы замка были довольно странными, может...",
        "Вы: Нужно проверить!"
        };

    public static bool isQuest3Open = false;
    public static bool isQuest3Complete = false;

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
            if(GUIController.QuestNumber == 7)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        isQuest3Open = false;
                        Dialog.SetActive(false);
                        isQuest3Complete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 7)
        {
            if(!isQuest3Complete)
            {
                isQuest3Open = true;
                Dialog.SetActive(true);
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
        isQuest3Open = false;
        Dialog.SetActive(false);
    }
}
