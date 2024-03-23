using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemData itemToDisplay; 
    [SerializeField] private  Image itemDisplayImage; // �κ��丮 ���� ������ �̹���
    [SerializeField] private  Image dragImage; // �巡�� ���϶� ��Ÿ���� �̹���
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

    // ���Ե� ǥ��
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
    
    // ��� ���� ���� ǥ��
    public void Display(ItemData itemDisplay)
    {
        if (itemDisplay != null)
        {
            itemToDisplay = itemDisplay;
            itemDisplayImage.sprite = itemDisplay.thumbnail;

            itemDisplayImage.gameObject.SetActive(true);
            return;
        }
        itemDisplayImage.gameObject.SetActive(false);
    }

    // ���� Ŭ���� �ش� ����â�� ����
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (itemToDisplay != null)
            UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    // �巡�� �Ҷ�
    public void OnDrag(PointerEventData eventData)
    {
        if (itemToDisplay != null)
        {
            if (inventoryType == InventoryType.Tool)
            {
                dragImage.sprite = itemToDisplay.thumbnail;
                dragImage.gameObject.SetActive(true);
                dragImage.transform.position = eventData.position; // �巡���̹��� ��ġ�� �巡�װ� �߻��� ��ġ�� ����

                itemDisplayImage.gameObject.SetActive(false);
            }
        }
    }
    // �巡�� ���� ��
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
                {
                    dragImage.gameObject.SetActive(false);
                    returnPosition();
                    itemDisplayImage.gameObject.SetActive(true);
                }

                dragImage.gameObject.SetActive(false);
                UIManager.Instance.renderInventory();
            }
        }
    }

    // ó�� �κ��丮 �������� �ʱ�ȭ�ϱ� ���� ��ġ
    private void returnPosition()
    {
        transform.SetParent(originalPosition);
    }

    // evenData�� HandSlot�� �浹 �Ǿ����� Ȯ��
    private bool checkCollision(PointerEventData eventData)
    {
        GraphicRaycaster ray = GetComponentInParent<GraphicRaycaster>(); // GraphicRaycaster�� ���� Canvas�� ����

        if (ray != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current); // �̺�Ʈ ������ ��ġ�� ���콺 Ŭ�� ��ġ�� ����
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
