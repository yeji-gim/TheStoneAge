using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemData itemToDisplay;
    [SerializeField] private  Image itemDisplayImage;
    [SerializeField] private  Image dragImage;
    [SerializeField] private  TMP_Text quantityText;
    [SerializeField] private  int slotIndex;
    [SerializeField] private InventoryType inventoryType;
    Transform originalPosition;
    int quantity;
    public enum InventoryType
    {
        Item, Tool
    }

    void Awake()
    {
        originalPosition = transform.parent;
    }

    public void Display(ItemSlotData itemSlot)
    {
        itemToDisplay = itemSlot.itemData;
        quantity = itemSlot.quantity;
        quantityText.text = "";
        if (itemToDisplay != null)
        {  
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            if (quantity > 1)
                quantityText.text = quantity.ToString();
            itemDisplayImage.gameObject.SetActive(true);
            return;
        }
       itemDisplayImage.gameObject.SetActive(false);
    }
    public void Display(ItemData itemDisplay)
    {
        if (itemDisplay != null)
        {
            this.itemToDisplay = itemDisplay;
            itemDisplayImage.sprite = itemDisplay.thumbnail;

            itemDisplayImage.gameObject.SetActive(true);
            return;
        }
        itemDisplayImage.gameObject.SetActive(false);

    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (itemToDisplay != null)
            UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemToDisplay != null)
        {
            if (inventoryType == InventoryType.Tool)
            {
                dragImage.sprite = itemToDisplay.thumbnail;
                dragImage.gameObject.SetActive(true);
                dragImage.transform.position = eventData.position;

                itemDisplayImage.gameObject.SetActive(false);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemToDisplay != null)
        {
            if (inventoryType == InventoryType.Tool)
            {
                itemDisplayImage.gameObject.SetActive(true);

                if (checkCollision(eventData))
                    InventoryManager.Instance.inventoryToHand(slotIndex, inventoryType);
                else
                    returnPosition();

                dragImage.gameObject.SetActive(false);
                UIManager.Instance.renderInventory();
            }
        }
    }
    private void returnPosition()
    {
        transform.SetParent(originalPosition);
    }
    private bool checkCollision(PointerEventData eventData)
    {
        GraphicRaycaster ray = GetComponentInParent<GraphicRaycaster>();

        if (ray != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = eventData.position;

            List<RaycastResult> results = new List<RaycastResult>();
            ray.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("HandSlot")) 
                    return true;
            }
        }

        return false;
    }
    public void assignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

}
