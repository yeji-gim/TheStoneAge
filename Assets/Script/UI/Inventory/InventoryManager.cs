using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
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
    [SerializeField] private GameObject handImage;
    [Header("Items")]
    [SerializeField]
    private ItemSlotData[] Items = new ItemSlotData[18];

    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        ItemData toolToEquip = tools[slotIndex].itemData;
        tools[slotIndex].itemData = equippedTool;
        equippedTool = toolToEquip;
              //Change the Inventory Slot to the Hand's

        ThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        
        //Change the Hand's Slot to the Inventory Slot's
        equippedTool = toolToEquip;
        if (toolToEquip.itemName == "¡÷∏‘µµ≥¢")
        {
            player.ChangeWeapon(1);
        }
        else if (toolToEquip.itemName == "µπµµ≥¢")
        {
            player.ChangeWeapon(2);
        }
        else if (toolToEquip.itemName == "√¢")
        {
            player.ChangeWeapon(3);
        }
        

        UIManager.Instance.RenderInventory();
    }

    public void HandToIventory()
    {
        ThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        player.ChangeWeapon(0);
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