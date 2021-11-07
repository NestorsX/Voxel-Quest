using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    float GeneralTimer;

    public Transform player;

    void Start()
    {
        GeneralTimer = PlayerPrefs.GetFloat("GeneralTimer");
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 && player.position.y<5)
        {
            PlayerController.speed = 3f;
            SceneManager.LoadScene(2);
        }
    }

    void FixedUpdate()
    {
        GeneralTimer += Time.deltaTime;
        PlayerPrefs.SetFloat("GeneralTimer", GeneralTimer);
    }
}
