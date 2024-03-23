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
    // 대화하기 버튼을 눌렀을 경우 다이얼로그 시작
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject sister = GameObject.FindGameObjectWithTag("sister");
        
        if (sister != null) startDialogue(sister);
    }
}
