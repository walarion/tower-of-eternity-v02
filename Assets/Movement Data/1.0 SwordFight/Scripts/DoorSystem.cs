using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSystem : MonoBehaviour
{
    public bool isPlayerEntered = false;
    public Text doorText;
    public int NextSceneNumber;

    private void Start()
    {
        GameManager.OnGameCompleted += OnGameDone;
    }
    private void OnDisable()
    {
        GameManager.OnGameCompleted -= OnGameDone;
    }
    private void OnGameDone()
    {
        doorText.text = "Unlocked";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.isGameCompleted) {
            doorText.text = "Door Locked. Kill All enemies";
            return;
        } 
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Open", true);
            isPlayerEntered = true;
            Invoke("OpenNextLevel", 2f);
        }
    }
    void OpenNextLevel() { GameManager.Instance.LoadNextLevel(NextSceneNumber); }
    private void OnTriggerExit(Collider other)
    {
        if(!GameManager.Instance.isGameCompleted) {
            doorText.text = "Door Locked.";
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
