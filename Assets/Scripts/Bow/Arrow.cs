using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    Vector3 lastpos;
    Rigidbody rb;
	public AudioClip Hit;
    public AudioClip Kill;

    RaycastHit hit;

    GameObject HitAlertContainer;

    bool complete = false;
    string objectName;
    string alertType;

    Transform targetLook;

    public static string CurrentTargetName;
    
    public Material matBlink;

    public static List<string> Targets = new List<string>() {
        "Ground", "Rock", "TreeLog", "Tree", "Cart", "Target", 
        "Portal", "Chicken", "alpaca", "dog", "frog", "CastleBLOCK", "CastleBalcony", "CastleDoor",
        "CTrigger"
        };

    void Start()
    {
        lastpos = transform.position;
        rb = GetComponent<Rigidbody>();
        HitAlertContainer = GameObject.Find("/GUI/HitAlert");
        targetLook = GameObject.Find("/Camera/TargetLook").GetComponent<Transform>();
        transform.LookAt(targetLook);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Physics.Linecast(lastpos, transform.position, out hit))
        {
            Debug.Log(hit.transform.name);
            if(Targets.Any(str => hit.transform.name.Contains(str)))
            {
                if(!complete)
                {
                    complete = true;
                    SuccesHit();
                }
            }
        }
        lastpos = transform.position;
    }
    
    void SuccesHit()
    {
        if(!hit.transform.name.Contains("Chicken"))
            PlayerController.PlayHitSound(Hit);
        rb.isKinematic = true;
        CurrentTargetName = hit.transform.name;
        if(hit.transform.name.Contains("Ground"))
        {
            objectName = "землю";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Rock"))
        {
            objectName = "скалу";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("TreeLog"))
        {
            objectName = "бревно";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Tree"))
        {
            objectName = "дерево";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Cart"))
        {
            objectName = "повозку";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Target"))
        {
            objectName = "мишень";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Portal"))
        {
            objectName = "портал";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("CastleBLOCK"))
        {
            objectName = "стену замка";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("CastleBalcony"))
        {
            objectName = "балкон замка";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("CastleDoor"))
        {
            objectName = "дверь замка";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("CTrigger"))
        {
            objectName = "цель";
            alertType = "Вы попали в ";
        }
        else if(hit.transform.name.Contains("Chicken"))
        {
            PlayerController.PlayHitSound(Kill);
            hit.transform.Find("default").GetComponent<MeshRenderer>().material = matBlink;
            Destroy(GameObject.Find(hit.transform.parent.name), 0.1f);
            Destroy(gameObject, 0.1f);
            objectName = "курицу";
            alertType = "Вы убили ";
        }
        else if(hit.transform.name.Contains("alpaca"))
        {
            PlayerController.PlayHitSound(Kill);
            hit.transform.Find("default").GetComponent<MeshRenderer>().material = matBlink;
            Destroy(GameObject.Find(hit.transform.parent.name), 0.1f);
            Destroy(gameObject, 0.1f);
            objectName = "альпаку";
            alertType = "Вы убили ";
        }
        else if(hit.transform.name.Contains("dog"))
        {
            PlayerController.PlayHitSound(Kill);
            hit.transform.Find("default").GetComponent<MeshRenderer>().material = matBlink;
            Destroy(GameObject.Find(hit.transform.parent.name), 0.1f);
            Destroy(gameObject, 0.1f);
            objectName = "собаку";
            alertType = "Вы убили ";
        }
        else if(hit.transform.name.Contains("frog"))
        {
            PlayerController.PlayHitSound(Kill);
            hit.transform.Find("default").GetComponent<MeshRenderer>().material = matBlink;
            Destroy(GameObject.Find(hit.transform.parent.name), 0.1f);
            Destroy(gameObject, 0.1f);
            objectName = "лягушку";
            alertType = "Вы убили ";
        }
        if(objectName!=null)
            HitAlert.Alert(alertType + objectName); 
        speed=0;
        Destroy(gameObject, 30f);
    }
}
