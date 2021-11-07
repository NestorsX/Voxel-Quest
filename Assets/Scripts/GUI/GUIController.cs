using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public GameObject QuestJournal;
    public List<GameObject> descriptions;
    bool isJournalActive = false;

    public GameObject questAlert;

    public static int QuestNumber = 0;

    public static float timer;
    public static bool isQuestAlertVisible = false;


    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            QuestNumber = 5;
        }
    }

    void Update()
    {   
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(isQuestAlertVisible)
        {
            if(!(timer > 0))
            {
                isQuestAlertVisible = false;
                NewQuestAlertOFF();
            }
        }


        if(!DeadMan.DeadManMonologOpen && !Woman.WomanDialogOpen)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if(!isJournalActive)
                {
                    QuestJournal.SetActive(true);
                    isJournalActive = true;
                }
                else
                {
                    QuestJournal.SetActive(false);
                    isJournalActive = false;
                }
            }
        }
        else
        {
            QuestJournal.SetActive(false);
        }



        if(LookAroundEvent.LookAroundEventEntered)
        {
            descriptions[0].SetActive(true);
            LookAroundEvent.LookAroundEventEntered = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(DeadMan.DeadManMonologComplete)
        {
            Destroy(descriptions[0]);
            descriptions[1].SetActive(true);
            DeadMan.DeadManMonologComplete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(PreShootZoneEvent.ShootZoneEventEntered)
        {
            Destroy(descriptions[1]);
            descriptions[2].SetActive(true);
            PreShootZoneEvent.ShootZoneEventEntered = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(ShootZone.ShootZoneComplete)
        {
            Destroy(descriptions[2]);
            descriptions[3].SetActive(true);
            ShootZone.ShootZoneComplete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Woman.WomanDialogComplete)
        {
            Destroy(descriptions[3]);
            descriptions[4].SetActive(true);
            Woman.WomanDialogComplete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest1.isQuest1Complete)
        {
            Destroy(descriptions[4]);
            descriptions[5].SetActive(true);
            Quest1.isQuest1Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest2.isQuest2Complete)
        {
            Destroy(descriptions[5]);
            descriptions[6].SetActive(true);
            Quest2.isQuest2Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest3.isQuest3Complete)
        {
            Destroy(descriptions[6]);
            descriptions[7].SetActive(true);
            Quest3.isQuest3Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest4.isQuest4Complete)
        {
            Destroy(descriptions[7]);
            descriptions[8].SetActive(true);
            Quest4.isQuest4Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest5.isQuest5Complete)
        {
            Destroy(descriptions[8]);
            descriptions[9].SetActive(true);
            Quest5.isQuest5Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
        if(Quest6.isQuest6Complete)
        {
            Destroy(descriptions[9]);
            descriptions[10].SetActive(true);
            Quest6.isQuest6Complete = false;
            QuestNumber++;
            timer = 5f;
            NewQuestAlert();
        }
    }

    void NewQuestAlert()
    {
        questAlert.SetActive(true);
        isQuestAlertVisible = true;
    }
    void NewQuestAlertOFF()
    {
        questAlert.SetActive(false);
    }
}
