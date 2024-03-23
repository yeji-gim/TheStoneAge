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
    public int index = 0;

    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }

    // 플레이어와 npc와 Trigger가 발생한 경우 대화하기 패널 활성화
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.isDialoguePromptActive();
            if (!isPanelActive)
                dialoguePanel.gameObject.SetActive(true);
        }
    }

    // 플레이어와 npc와 Trigger가 끝났을 경우 대화하기 패널 비활성화
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.isDialoguePromptActive();
            if (!isPanelActive)
                dialoguePanel.gameObject.SetActive(false);
        }
    }

    // Quest가 완료되었는지 확인
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
