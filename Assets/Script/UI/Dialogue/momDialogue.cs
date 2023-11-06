using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class momDialogue : baseDialogue
{
    public GameObject camera;
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        StartDialogue(GameObject.FindGameObjectWithTag("momquest"), GameObject.FindGameObjectWithTag("mom"), npccontroller.charcterData);
    }
}
