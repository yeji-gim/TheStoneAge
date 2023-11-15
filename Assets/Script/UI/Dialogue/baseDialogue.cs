using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    bool isTwo = false;
    bool isOne = false;
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
    protected void checkQuest(GameObject questObject, GameObject quest)
    {
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        QuestManager questManager = questObject.GetComponent<QuestManager>();
        npcController npccontroller = quest.GetComponent<npcController>();
        if (questManager.quest[0].CheckCompletion(inventoryItemSlots))
        {
            UIManager.Instance.ShowQuestCompletionPanel();
            isOne = true;
            index = 1;
        }

        if (questManager.quest[1].CheckCompletion(inventoryItemSlots))
        {
            UIManager.Instance.ShowQuestCompletionPanel();
            isTwo = true;
        }
        
    }

    protected void StartDialogue(GameObject questObject, GameObject quest)
    {
        QuestManager questManager = questObject.GetComponent<QuestManager>();
        npcController npccontroller = quest.GetComponent<npcController>();
        if (DialogueManager.Instance != null)
        {
            if (isTwo)
            {
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.completedialogueLines);
                isTwo = true;
                return;
            }
            else if (isOne)
            {
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.secondQuestdialogueLines);
                return;
            }
            DialogueManager.Instance.StartDialogue(npccontroller.charcterData.firstquestdialogueLines);
        }
    }
}
