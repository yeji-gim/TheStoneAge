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
    ItemSlotData[] inventoryEquipmentSlots;
    private void Start()
    {
        iscomplete = false;
        isquesting = false;
        getquest = false;
        
    }
    private void Update()
    {
        Debug.Log(currentQuestIndex);
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        isComplete();
    }
    public void isComplete()
    {
        if (getquest)
        {
            quest[currentQuestIndex].CheckCompletion(inventoryItemSlots);
            if (quest[1].isCompleted)
            {
                iscomplete = true;
                isquesting = true;

            }
            else if (quest[0].isCompleted)
            {
                isquesting = true;
                currentQuestIndex = 1;
               
            }         
            else
            {
                iscomplete = false;
                isquesting = true;
            }
        }
    }

    public void getQuest()
    {
        getquest = true;
        UIManager.Instance.button.gameObject.SetActive(false);
        UIManager.Instance.DisplayQuest(quest[currentQuestIndex]);
    }

    public bool getisQuesting()
    {
        return isquesting;
    }

    public bool getisCompleting()
    {
        return iscomplete;
    }

    public int getCurrentIndex()
    {
        return currentQuestIndex;
    }
}