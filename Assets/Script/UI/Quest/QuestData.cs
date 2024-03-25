using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quest/Quest")]
public class QuestData : ScriptableObject
{
    public string questname;
    [TextArea(15, 20)]
    public string description;

    public List<RequiredItem> requiredItems; // 퀘스트 리스트
    public bool isCompleted;

    [System.Serializable]
    public class RequiredItem // 퀘스트 내용
    {
        public ItemData item;
        public int itemCount;
    }
    public bool checkCompletion(ItemSlotData[] inventorySlots)
    {
        isCompleted = true;

        foreach (RequiredItem requiredItem in requiredItems)
        {
            ItemData requiredItemData = requiredItem.item;
            int requiredItemCount = requiredItem.itemCount;
            bool itemFull = false; 
            foreach (ItemSlotData inventorySlot in inventorySlots)
            {
                if (inventorySlot == null)
                {
                    continue;
                }
                if (inventorySlot.itemData == requiredItemData)
                {
                    int inventoryItemCount = inventorySlot.quantity;
                    if (inventoryItemCount >= requiredItemCount)
                    {
                        itemFull = true;
                        break;
                    }
                }
            }

            if (!itemFull)
            {
                isCompleted = false; 
                break; 
            }
        }
        return isCompleted;
    }
}
