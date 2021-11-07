using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest6 : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    int counter;
    string[] lines = new string[] {
        "Вы: Опять портал...",
        "Вы: Я начинаю их ненавидеть",
        "Вы: ...",
        "Вы: Ну что ж, другого выбора у меня нет..."
        };

    public static bool isQuest6Open = false;
    public static bool isQuest6Complete = false;

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
            if(GUIController.QuestNumber == 10)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        isQuest6Open = false;
                        Dialog.SetActive(false);
                        isQuest6Complete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 10)
        {
            if(!isQuest6Complete)
            {
                isQuest6Open = true;
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
        isQuest6Open = false;
        Dialog.SetActive(false);
    }
}
