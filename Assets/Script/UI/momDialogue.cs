using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class momDialogue : MonoBehaviour
{

    npcController npccontroller;

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Gemtmousebutton");
                OnInteractablenpccontroller(hit);
            }
        }
    }

    void OnInteractablenpccontroller(RaycastHit hit)
    {

        Collider other = hit.collider;
        if (hit.collider.CompareTag("mom"))
        {
            GameObject momObject = GameObject.FindGameObjectWithTag("momquest");
            QuestManager questManager = momObject.GetComponent<QuestManager>();
            npccontroller = hit.collider.GetComponent<npcController>();
            Debug.Log($"isquesting in momDialogue {questManager.getisQuesting()}"); // 이 줄을 추가
            if (npccontroller != null)
            {
                if (DialogueManager.Instance != null)
                {
                    Debug.Log($"isquesting {questManager.getisQuesting()} && isComplete{questManager.getisCompleting()}");
                    if (questManager.getisCompleting() == false&& questManager.getisQuesting() == false)
                    {
                        DialogueManager.Instance.StartDialogue(npccontroller.charcterData.dialogueLines);
                    }
                        
                    else if (questManager.getisQuesting() == true && questManager.getisCompleting() == true)
                    {
                        DialogueManager.Instance.StartDialogue(npccontroller.charcterData.completedialogueLines);
                    }
                    // 퀘스트가 통과하지 않았을 때
                    else if (questManager.getisQuesting() == true && questManager.getisCompleting() == false)
                    {
                        DialogueManager.Instance.StartDialogue(npccontroller.charcterData.noncompletedialogueLines);
                    }
                    Debug.Log("mom1");
                }

            }
            else
            {
                Debug.LogWarning("npccontroller ");
            }
        }
    }
   
}
