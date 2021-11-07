using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    int counter;
    string[] lines = new string[] {
        "Вы: Портал из которого я вышел..",
        "Вы: Вряд-ли я смогу его активировать.",
        "Вы: Кажется назад пути нет..."
        };

    public static bool isQuest2Open = false;
    public static bool isQuest2Complete = false;

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
            if(GUIController.QuestNumber == 6)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        isQuest2Open = false;
                        Dialog.SetActive(false);
                        isQuest2Complete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 6)
        {
            if(!isQuest2Complete)
            {
                isQuest2Open = true;
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
        isQuest2Open = false;
        Dialog.SetActive(false);
    }
}
