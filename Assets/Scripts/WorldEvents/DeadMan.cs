using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DeadMan : MonoBehaviour
{
    public GameObject DeadManMono;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    public GameObject AlertPoint;

    int counter;
    string[] lines = new string[] {
        "Вы: Оxx.. это ведь мужик, который вёз меня домой...",
        "Вы: Не понимаю что с нами случилось в дороге..",
        "Вы: Кажется ему уже нечем помочь. Очень жаль..."};

    public static bool DeadManMonologOpen = false;
    public static bool DeadManMonologComplete = false;

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
            if(GUIController.QuestNumber == 1)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        Destroy(AlertPoint, 0f);
                        DeadManMonologOpen = false;
                        DeadManMono.SetActive(false);
                        DeadManMonologComplete = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 1)
        {
            if(!DeadManMonologComplete)
            {
                DeadManMonologOpen = true;
                DeadManMono.SetActive(true);
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
        DeadManMonologOpen = false;
        DeadManMono.SetActive(false);
    }
}
