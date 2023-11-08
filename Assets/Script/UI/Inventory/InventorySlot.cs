using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemToDisplay;
    int quantity;
    public Image itemDisplayImage;
    public Image dragImage;
    public TMP_Text quantityText;
    public int slotIndex;
    Transform originalPosition;

    public enum InventoryType
    {
        Item, Tool
    }

    public InventoryType inventoryType;
    
    private RectTransform rectTransform;

    void Awake()
    {
        originalPosition = transform.parent;
        rectTransform = GetComponent<RectTransform>();
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
            {
                quantityText.text = quantity.ToString();
            }
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
        {
            UIManager.Instance.DisplayItemInfo(itemToDisplay);
        }
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

                // 원래 아이템 이미지 숨김
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

                if (CheckForCollision(eventData))
                {
                    Debug.Log("아이템");
                    InventoryManager.Instance.InventoryToHand(slotIndex,inventoryType);
                }
                else
                {

                    ReturnToOriginalPosition();
                }

                dragImage.gameObject.SetActive(false);
                UIManager.Instance.RenderInventory();
            }
        }
    }
    private void ReturnToOriginalPosition()
    {
        transform.SetParent(originalPosition);
    }
    private bool CheckForCollision(PointerEventData eventData)
    {
        GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();

        if (raycaster != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = eventData.position;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("HandSlot"))
                {
                    return true;
                }
            }
        }

        return false;
    }
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

}
