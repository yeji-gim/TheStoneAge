using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int currentQuestIndex = 0;
    private bool iscomplete;
    private bool isquesting;
    private bool getquest;
    public QuestData[] quest;
    ItemSlotData[] inventoryItemSlots;
    private void Start()
    {
        iscomplete = false;
        isquesting = false;
        getquest = false;
        
    }
    private void Update()
    {
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        isComplete();
    }
    public void isComplete()
    {
        if (getquest)
        {
            quest[currentQuestIndex].CheckCompletion(inventoryItemSlots);
            if (quest[currentQuestIndex].isCompleted)
            {

                isquesting = true;
                iscomplete = true;
                if (currentQuestIndex < quest.Length - 1)
                {
                    currentQuestIndex++;
                    UIManager.Instance.DisplayQuest(quest[currentQuestIndex]);
                }
            }
            else
            {
                isquesting = true;
                iscomplete = false;
                
            }
        }
    }

    public void getQuest()
    {
        
        getquest = true;
        UIManager.Instance.button.gameObject.SetActive(false);
        UIManager.Instance.DisplayQuest(quest[currentQuestIndex]);
        Debug.Log($"isquesting {getquest}");
    }

    public bool getisQuesting()
    {
        return isquesting;
    }

    public bool getisCompleting()
    {
        return iscomplete;
    }
}