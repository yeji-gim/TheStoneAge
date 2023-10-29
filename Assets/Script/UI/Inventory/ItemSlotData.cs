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
        AddQuantity(1);
    }
    public void AddQuantity(int amountToAdd)
    {
        quantity += amountToAdd;
    }
   
    public void Remove(int count)
    {
        quantity -= count;
        ValidateQuantity();
    }

    private void ValidateQuantity()
    {
        if (quantity <= 0 || itemData == null)
        {
            Empty();
        }
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
        // 현재 슬롯의 아이템이 비어 있지 않고, 이름이 같은 경우에만 스택 가능
        return !IsEmpty() && itemToCompare != null && itemToCompare.name == itemData.name;
    }
}
