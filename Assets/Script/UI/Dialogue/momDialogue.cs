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
        camera.SetActive(true);
        GameObject momQuest = GameObject.FindGameObjectWithTag("momquest");
        GameObject mom = GameObject.FindGameObjectWithTag("mom");

        if (momQuest != null && mom != null)
        {
            StartDialogue(momQuest, mom);
        }
        else
        {
            Debug.Log("오브젝트를 찾지 못한 경우에 대한 처리");
        }
    }
}
