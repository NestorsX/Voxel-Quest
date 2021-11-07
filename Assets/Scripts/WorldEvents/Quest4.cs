using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4 : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    int counter;
    string[] lines = new string[] {
        "Вы: Эта платформа здесь явно не просто так..",
        "Вы: С нее открывается хороший вид на балконы.",
        "Вы: Может быть внутри этих балконов есть какие-то рычаги?",
        "Вы: Наверняка это какой-то шифр...",
        };

    public static bool isQuest4Open = false;
    public static bool isQuest4Complete = false;

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
            if(GUIController.QuestNumber == 8)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        isQuest4Open = false;
                        Dialog.SetActive(false);
                        isQuest4Complete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 8)
        {
            if(!isQuest4Complete)
            {
                isQuest4Open = true;
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
        isQuest4Open = false;
        Dialog.SetActive(false);
    }
}
