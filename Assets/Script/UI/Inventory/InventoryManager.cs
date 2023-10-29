using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public InventorySlot inventorySlot;
    [Header("Tools")]
    [SerializeField] private ItemSlotData[] tools = new ItemSlotData[6];
    [SerializeField] public ItemData equippedTool = null;

    [Header("Items")]
    [SerializeField]
    private ItemSlotData[] Items = new ItemSlotData[18];

    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        ItemData toolToEquip = tools[slotIndex].itemData;

        //Change the Inventory Slot to the Hand's
        tools[slotIndex].itemData = equippedTool;

        //Change the Hand's Slot to the Inventory Slot's
        equippedTool = toolToEquip;
        UIManager.Instance.RenderInventory();
    }

    public void HandToIventory()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (tools[i].itemData == null)
            {
                tools[i].itemData = equippedTool;
                equippedTool = null;
                break;
            }
        }
        
        UIManager.Instance.RenderInventory();
    }

    public ItemSlotData[] GetInventorySlots(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            return Items;
        }

        return tools;
    }
    public void ConsumeItem(ItemSlotData itemSlot, int count)
    {
        if (itemSlot.IsEmpty())
        {
            Debug.Log("There is nothing to consume!");
            return;
        }
        itemSlot.Remove(count);
        UIManager.Instance.RenderInventory();
    }
}