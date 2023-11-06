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
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        inventoryEquipmentSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        isComplete();
    }
    public void isComplete()
    {
        if (getquest)
        {
            quest[currentQuestIndex].CheckCompletion(inventoryItemSlots);
            quest[currentQuestIndex].CheckCompletion(inventoryEquipmentSlots);
            if (quest[currentQuestIndex].isCompleted)
            {
                Debug.Log($"isCompleted true¿‘¥œ¥Ÿ");
                isquesting = true;
                currentQuestIndex++;
            }
            else if(quest[quest.Length-1].isCompleted)
            {
                iscomplete = true;
                isquesting = true;
                
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

    public int getCurrentIndex()
    {
        return currentQuestIndex;
    }
}