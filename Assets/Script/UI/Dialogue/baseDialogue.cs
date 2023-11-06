using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public npcController npccontroller;
    Ray ray;

    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(false);
            }
        }
    }

    protected void StartDialogue(GameObject questObject, GameObject npc, CharacterData characterData)
    {
        QuestManager questManager = questObject.GetComponent<QuestManager>();
        npccontroller = npc.GetComponent<npcController>();

        if (DialogueManager.Instance != null)
        { 
            if (questManager.getisQuesting() == false && questManager.getCurrentIndex() == 0)
            {
                DialogueManager.Instance.StartDialogue(characterData.firstquestdialogueLines);
            }
            else if (questManager.getisQuesting() == true && questManager.getCurrentIndex() == 1)
            {
                DialogueManager.Instance.StartDialogue(characterData.secondQuestdialogueLines);
            }
            else if (questManager.getisQuesting() == true && questManager.getisCompleting() == true)
            {
                DialogueManager.Instance.StartDialogue(characterData.completedialogueLines);
            }
            else if (questManager.getisQuesting() == true && questManager.getisCompleting() == false)
            {
                DialogueManager.Instance.StartDialogue(characterData.noncompletedialogueLines);
            }
        }
    }

  
}
