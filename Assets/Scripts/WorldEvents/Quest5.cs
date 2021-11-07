using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest5 : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip sound;

    public GameObject Dialog;
    public GameObject TextMeshPro;
    TMPro.TMP_Text text;

    private List<string> targets = new List<string>() {"CTrigger1", "CTrigger2", "CTrigger3", "CTrigger4", "CTrigger5", "CTrigger6"};
    public int[] key = new int[] {0, 0, 0, 0, 0, 0};
    public List<bool> isActive = new List<bool>() {true, true, true, true, true, true};
    public int targetCounter = 0;

    public Transform MainCastleDoor;

    public int counter;
    string[] lines = new string[] {
        "Вы: Да! Что-то произошло и эта дверь открылась!",
        "Вы: Это было сложно, пора двигаться дальше.."
        };

    public static bool isQuest5Open = false;
    public static bool isQuest5Complete = false;
    bool isKeyAccepted = false;

    bool isOnTriggerStay = false;

    void Start()
    {
        text = TextMeshPro.GetComponent<TMPro.TMP_Text>();
        Restart();
    }
    
    void Restart()
    {
        MainCastleDoor.position = new Vector3(MainCastleDoor.position.x, 39.3f, MainCastleDoor.position.z);
        Arrow.CurrentTargetName = "";
        targetCounter = 0;
        key = new int[] {0, 0, 0, 0, 0, 0};
        isActive = new List<bool>() {true, true, true, true, true, true};
        for(int i=0; i<3; i++)
        {
            int curr;
            bool isPositive = false;
            while (!isPositive)
            {
                curr = Random.Range(0, 6);
                if(key[curr]!=1)
                {
                    key[curr] = 1;
                    isPositive = true;
                }
            }
        }
    }

    void Update()
    {
        if(isOnTriggerStay)
        {
            if(GUIController.QuestNumber == 9)
            {
                if (Input.GetKeyDown(KeyCode.F) )
                {
                    counter++;
                    if(counter<lines.Length)
                        text.text = lines[counter];
                    else{
                        isQuest5Open = false;
                        Dialog.SetActive(false);
                        isQuest5Complete = true;
                    }
                }
            }
            if(!isKeyAccepted)
            {
                for(int i=0; i<targets.Count; i++)
                {
                    if(targets[i] == Arrow.CurrentTargetName && key[i] == 1 && isActive[i] == true)
                    {
                        audio.PlayOneShot(sound);
                        targetCounter++;
                        isActive[i] = false;
                        MainCastleDoor.position = new Vector3(MainCastleDoor.position.x, MainCastleDoor.position.y-3f, MainCastleDoor.position.z);
                    }
                    else if(targets[i] == Arrow.CurrentTargetName && key[i] == 0)
                    {
                        Restart();
                    }
                }
            }
        }
        else
        {
            Arrow.CurrentTargetName = "";
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 9)
        {
            if(!isKeyAccepted)
            {
                targetCounter = 0;
            }
            if(!isQuest5Complete && isKeyAccepted)
            {
                isQuest5Open = true;
                Dialog.SetActive(true);
                counter = 0;
                text.text = lines[counter];
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        isOnTriggerStay = true;
        if(targetCounter == 3)
        {
            isKeyAccepted = true;
            targetCounter = 0;
            isQuest5Open = true;
            isQuest5Open = true;
            Dialog.SetActive(true);
            counter = 0;
            text.text = lines[counter];
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        isOnTriggerStay = false;
        isQuest5Open = false;
        Dialog.SetActive(false);
    }
}
