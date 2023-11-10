using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ItemData item;

    public void oldItem()
    {
        /*bool isPanelActive = UIManager.Instance.IsInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
 

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Stackable(item))
                {
                    // 같은 아이템이 이미 있으면 수량을 추가
                    items[i].AddQuantity();
                    break;
                }
            }

            UIManager.Instance.RenderInventory();
        */
            Destroy(gameObject);
        
    }
    public void newItem()
    {
        bool isPanelActive = UIManager.Instance.IsInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);


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
        // 인벤토리를 다시 렌더링하고 현재 게임 오브젝트를 제거합니다.
        UIManager.Instance.RenderInventory();

        Destroy(gameObject);
    }
}
