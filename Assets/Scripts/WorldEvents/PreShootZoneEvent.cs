using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreShootZoneEvent : MonoBehaviour
{
    public static bool ShootZoneEventEntered = false;

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber == 2)
        {
            ShootZoneEventEntered = true;
            Destroy(gameObject, 3f);
        }
    }
}
