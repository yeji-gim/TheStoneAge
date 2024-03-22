using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ItemData item;

    public void oldItem()
    {
        Destroy(gameObject);
    }
    public void newItem()
    {
        bool isPanelActive = UIManager.Instance.isInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);


            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemData == null)
                {
                    items[i].itemData = item;
                    items[i].AddQuantity();
                    item = null;
                    break;
                }
            }
        }
        UIManager.Instance.renderInventory();

        Destroy(gameObject);
    }
}
