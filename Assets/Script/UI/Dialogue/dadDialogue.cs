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
    // 대화하기 버튼을 눌렀을 경우 다이얼로그 시작
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject dad = GameObject.FindGameObjectWithTag("dad");

        if (dad != null) startDialogue(dad);
    }
}
