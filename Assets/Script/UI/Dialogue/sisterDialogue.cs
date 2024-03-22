using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisterDialogue : baseDialogue
{
    public GameObject camera;

    private void Update()
    {
        if(DialogueManager.Instance.sisterQuest!=null) checkQuest(DialogueManager.Instance.sisterQuest);
    }
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        
        if (sister != null) startDialogue(sister);
    }
}
