using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemSlotData
{
    public ItemData itemData;
    public int quantity;

    public ItemSlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        ValidateQuantity();
    }

    public ItemSlotData(ItemData itemData)
    {
        this.itemData = itemData;
        quantity = 1;
        ValidateQuantity();
    }

    public void AddQuantity()
    {
        quantity += 1;
    }

    public void Remove()
    {
        quantity -= 1;
        ValidateQuantity();
    }

    private void ValidateQuantity()
    {
        if (quantity <= 0 || itemData == null) Empty();
    }

    public void Empty()
    {
        itemData = null;
        quantity = 0;
    }

    public bool IsEmpty()
    {
        return itemData == null;
    }
    public bool Stackable(ItemData itemToCompare)
    {
        return !IsEmpty() && itemToCompare != null && itemToCompare.name == itemData.name;
    }
}
