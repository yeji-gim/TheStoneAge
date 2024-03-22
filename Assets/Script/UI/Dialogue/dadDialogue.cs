using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dadDialogue : baseDialogue
{
    public GameObject camera;

    private void Update()
    {
        if(DialogueManager.Instance.dadQuest != null) checkQuest(DialogueManager.Instance.dadQuest);
    }
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject dad = GameObject.FindGameObjectWithTag("dad");

        if (dad != null) startDialogue(dad);
    }
}
