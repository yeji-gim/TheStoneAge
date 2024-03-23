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
    // 대화하기 버튼을 눌렀을 경우 다이얼로그 시작
    public void acceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject grandfather = GameObject.FindGameObjectWithTag("grandfather");
        if (grandfather != null) startDialogue(grandfather);
    }
}
