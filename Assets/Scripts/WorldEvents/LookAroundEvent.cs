using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundEvent : MonoBehaviour
{
    public static bool LookAroundEventEntered = false;

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 0)
        {
            LookAroundEventEntered = true;
            Destroy(gameObject, 3f);
        }
    }

}
