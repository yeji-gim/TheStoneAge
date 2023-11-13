using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    ItemSlotData[] inventoryItemSlots;
    bool isTwo = false;
    bool isOne = false;
    Ray ray;
    public int index = 0;

    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
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
    
    protected void StartDialogue(GameObject questObject, GameObject quest)
    {
        QuestManager questManager = questObject.GetComponent<QuestManager>();
        npcController npccontroller = quest.GetComponent<npcController>();
        if (questManager.quest[0].CheckCompletion(inventoryItemSlots))
        {
            isOne = true; index = 1;
        }
        if (questManager.quest[1].CheckCompletion(inventoryItemSlots))
        {
            isTwo = true;
        }
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
