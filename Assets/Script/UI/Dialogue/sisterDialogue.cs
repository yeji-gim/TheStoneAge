using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisterDialogue : baseDialogue
{
    public GameObject camera;

    private void Update()
    {
        GameObject sisterQuest = GameObject.FindGameObjectWithTag("sisterquest");
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        checkQuest(sisterQuest, sister);
    }
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject sisterQuest = GameObject.FindGameObjectWithTag("sisterquest");
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        
        if (sisterQuest != null && sister != null)
        {
            StartDialogue(sisterQuest, sister);
        }
        else
        {
            Debug.Log("오브젝트를 찾지 못한 경우에 대한 처리");
        }
    }
}
