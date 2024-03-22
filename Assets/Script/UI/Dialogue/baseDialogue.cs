using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    bool isTwo = false;
    bool isOne = false;
    static bool questCompleted1Shown = false;
    static bool questCompleted2Shown = false;
    Ray ray;
    public int index = 0;

    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.isDialoguePromptActive();
            if (!isPanelActive)
                dialoguePanel.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.isDialoguePromptActive();
            if (!isPanelActive)
                dialoguePanel.gameObject.SetActive(false);
        }
    }
    protected void checkQuest(QuestData[] questObject)
    {
        
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);
        if (questObject[0].checkCompletion(inventoryItemSlots))
        {
            if (!questCompleted1Shown)
            {
                UIManager.Instance.showQuestCompletionPanel();
                questCompleted1Shown = true;
            }
            isOne = true;
            index = 1;
        }

        if (questObject[1].checkCompletion(inventoryItemSlots))
        {
            if (!questCompleted2Shown)
            {
                UIManager.Instance.showQuestCompletionPanel();
                questCompleted2Shown = true;
            }
            isTwo = true;
        }
        
    }

    protected void startDialogue(GameObject quest)
    {
        npcController npccontroller = quest.GetComponent<npcController>();
        if (DialogueManager.Instance != null)
        {
            if (isTwo)
            {
                DialogueManager.Instance.startDialogue(npccontroller.charcterData.completedialogueLines);
                isTwo = true;
                return;
            }
            else if (isOne)
            {
                DialogueManager.Instance.startDialogue(npccontroller.charcterData.secondQuestdialogueLines);
                return;
            }
            DialogueManager.Instance.startDialogue(npccontroller.charcterData.firstquestdialogueLines);
        }
    }
}
