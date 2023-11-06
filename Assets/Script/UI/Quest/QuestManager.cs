using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int currentQuestIndex = 0;
    private bool iscomplete;
    private bool isquesting;
    private bool getquest;
    private bool isFirst = false;
    private bool isSecond = false;
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
            if (quest[2].isCompleted && isFirst == true && isSecond && true)
            {
                iscomplete = true;
                isquesting = true;

            }
            else if (quest[1].isCompleted && isFirst == true && isSecond == false)
            {
                Debug.Log($"isCompleted 2¿‘¥œ¥Ÿ");
                isquesting = true;
                isSecond = true;
                currentQuestIndex = 2;

            }
            else if (quest[0].isCompleted && isFirst == false)
            {
                isquesting = true;
                isFirst = true;
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