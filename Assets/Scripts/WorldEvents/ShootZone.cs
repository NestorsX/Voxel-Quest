using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootZone : MonoBehaviour
{
    public GameObject ShootZoneBox;
    public GameObject ShootZoneText;
    private Text text;
    private int counter;
    private List<string> targets = new List<string>() {"Target", "Target (1)", "Target (2)", "Target (3)", "Target (4)"};
    private List<bool> isActive = new List<bool>() {true, true, true, true, true};

    public static bool ShootZoneComplete;

    public GameObject AlertPoint;

    void Start()
    {
        text = ShootZoneText.GetComponent<Text>();
    }

    void Update()
    {
        for(int i=0; i<targets.Count; i++)
        {
            if(targets[i] == Arrow.CurrentTargetName && isActive[i] == true)
            {
                counter++;
                text.text = "Цели: " + counter + "/5";
                isActive[i] = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 3)
        {
            if(!ShootZoneComplete)
            {
                Arrow.CurrentTargetName = "";
                ShootZoneBox.SetActive(true);
                for(int i=0; i<isActive.Count; i++)
                {
                    isActive[i] = true;
                }
                counter = 0;
                text.text = "Цели: " + counter + "/5";
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(GUIController.QuestNumber == 3)
        {
            if(counter==5)
            {
                Destroy(AlertPoint, 0f);
                ShootZoneComplete = true;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        ShootZoneBox.SetActive(false);
    }
}
