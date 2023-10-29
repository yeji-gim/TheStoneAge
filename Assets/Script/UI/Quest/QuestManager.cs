using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private bool iscomplete;
    private bool isquesting;
    private bool getquest;
    public QuestData quest;
    ItemSlotData[] inventoryItemSlots;
    private void Start()
    {
        iscomplete = false;
        isquesting = false;
        getquest = false;
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
    }
    private void Update()
    {
        isComplete();

    }
    public void isComplete()
    {
        quest.CheckCompletion(inventoryItemSlots);
        if (quest.isCompleted)
        {
            if (getquest)
            {
                isquesting = true;
                iscomplete = true;
            }
        }
        else
        {
            if (getquest)
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
        UIManager.Instance.DisplayQuest(quest);
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