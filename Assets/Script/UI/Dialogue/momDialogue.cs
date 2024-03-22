using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class momDialogue : baseDialogue
{
    public GameObject camera;
    private void Update()
    {
        if(DialogueManager.Instance.momQuest != null) checkQuest(DialogueManager.Instance.momQuest);

    }
    public void acceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject momQuest = GameObject.FindGameObjectWithTag("momquest");

        if (momQuest != null) startDialogue(momQuest);
    }
}
