using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ItemData item;

    // 아이템을 습득했을때 이전에 존재했던 아이템인 경우 그냥 Destroy
    public void oldItem()
    {
        // 아이템 갯수 증가 부분은 Farming() 메서드에서 진행
        Destroy(gameObject);
    }

    // 아이템을 습득했을때 새로운 아이템인 경우 인벤토리 슬롯에 아이템 추가
    public void newItem()
    {
        bool isPanelActive = UIManager.Instance.isInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);
            for (int i = 0; i < items.Length; i++)
            {
                // 비어있는 슬롯 확인
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
