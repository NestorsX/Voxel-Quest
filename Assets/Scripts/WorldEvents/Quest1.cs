using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public static bool isQuest1Complete = false;

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 5)
        {
            isQuest1Complete = true;
        }
    }
}
