using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisterDialogue : baseDialogue
{
    public GameObject camera;
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        StartDialogue(GameObject.FindGameObjectWithTag("sisterquest"), GameObject.FindGameObjectWithTag("sister"), npccontroller.charcterData);
    }
}
