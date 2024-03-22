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
    [SerializeField] private ItemSlotData[] Items = new ItemSlotData[18];

    public void inventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        ItemData toolToEquip = tools[slotIndex].itemData;
        tools[slotIndex].itemData = equippedTool;
        equippedTool = toolToEquip;

        ThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        
        equippedTool = toolToEquip;
        if (equippedTool != null && player != null)
        {
            if (toolToEquip.itemName == "¡÷∏‘µµ≥¢") player.ChangeWeapon(1);
            else if (toolToEquip.itemName == "µπµµ≥¢") player.ChangeWeapon(2);
            else if (toolToEquip.itemName == "√¢") player.ChangeWeapon(3);
            else player.ChangeWeapon(0);
        }
        UIManager.Instance.renderInventory();
    }

    public void handToIventory()
    {
        ThirdPersonController player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        if (equippedTool != null && player != null) player.ChangeWeapon(0);
        for (int i = 0; i < tools.Length; i++)
        {
            if (tools[i].itemData == null)
            {
                tools[i].itemData = equippedTool;
                equippedTool = null;
                break;
            }
        }

        UIManager.Instance.renderInventory();
    }

    public ItemSlotData[] getInventorySlots(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item) 
            return Items;

        return tools;
    }
}