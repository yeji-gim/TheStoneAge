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
    }
    public void getQuest()
    { 
        getquest = true;
        

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