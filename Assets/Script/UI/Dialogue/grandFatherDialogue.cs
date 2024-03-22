using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandFatherDialogue : baseDialogue
{
    public GameObject camera;

    private void Update()
    {
        if(DialogueManager.Instance.grandFatherQuest != null ) checkQuest(DialogueManager.Instance.grandFatherQuest);
    }

    public void acceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject grandfather = GameObject.FindGameObjectWithTag("grandfather");
        if (grandfather != null) startDialogue(grandfather);
    }
}
