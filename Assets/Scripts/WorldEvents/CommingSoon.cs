using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommingSoon : MonoBehaviour
{
    public static float timer;

    public GameObject PortalFader;
    Image img;

    float a = 0;

    bool timerStart = false;

    float step;
    
    void Start()
    {
        img = PortalFader.GetComponent<Image>();
        timer = 3f;
        step = 0.5882353f / timer;
    }

    void Update()
    {
        if(timerStart)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                a += step * Time.deltaTime;
                img.color = new Color (0.01685286f, 0f, 1f, a);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(GUIController.QuestNumber==5)
        {
            PortalFader.SetActive(true);
            timer = 3f;
            timerStart = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(GUIController.QuestNumber==5)
        {
            if(!(timer > 0))
            {
                SceneManager.LoadScene("LVL2");
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        PortalFader.SetActive(false);
        img.color = new Color (0.01685286f, 0f, 1f, 0.0f);
        a = 0;
        timerStart = false;
    }
}
