using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quest/Quest")]
public class QuestData : ScriptableObject
{
    public string questname;
    [TextArea(15, 20)]
    public string description;

    public List<RequiredItem> requiredItems;
    public bool isCompleted;

    [System.Serializable]
    public class RequiredItem
    {
        public ItemData item;
        public int itemCount;
    }
    public void CheckCompletion(ItemSlotData[] inventorySlots)
    {

        foreach (RequiredItem requiredItem in requiredItems)
        {
            isCompleted = false;

            ItemData requiredItemData = requiredItem.item;
            int requiredItemCount = requiredItem.itemCount;
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
                        isCompleted = true;
                        break;
                    }
                }
            }
        }
    }
}
