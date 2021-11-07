using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitAlert : MonoBehaviour
{
    public static bool isOpen = false;
    public GameObject HitAlertBox;

    public static float timer;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(isOpen)
        {
            if(!(timer > 0))
            {
                isOpen = false;
                OffAlert();
            }
        }
    }
    public static void Alert(string alert)
    {
        GameObject.Find("/GUI/HitAlert/HitAlertBox").SetActive(true);
        GameObject.Find("/GUI/HitAlert/HitAlertBox/HitAlertText").GetComponent<Text>().text = alert;
        isOpen = true;
        timer = 5f;
    }

    void OffAlert()
    {
        HitAlertBox.SetActive(false);
    }
}
