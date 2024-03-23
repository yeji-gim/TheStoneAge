using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemData itemToDisplay; 
    [SerializeField] private  Image itemDisplayImage; // 인벤토리 슬롯 아이템 이미지
    [SerializeField] private  Image dragImage; // 드래그 중일때 나타나는 이미지
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

    // 슬롯들 표시
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
    
    // 장비 장착 슬롯 표시
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

    // 슬롯 클릭시 해당 설명창이 나옴
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (itemToDisplay != null)
            UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    // 드래그 할때
    public void OnDrag(PointerEventData eventData)
    {
        if (itemToDisplay != null)
        {
            if (inventoryType == InventoryType.Tool)
            {
                dragImage.sprite = itemToDisplay.thumbnail;
                dragImage.gameObject.SetActive(true);
                dragImage.transform.position = eventData.position; // 드래그이미지 위치를 드래그가 발생한 위치에 설정

                itemDisplayImage.gameObject.SetActive(false);
            }
        }
    }
    // 드래그 끝날 때
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

    // 처음 인벤토리 슬롯으로 초기화하기 위한 위치
    private void returnPosition()
    {
        transform.SetParent(originalPosition);
    }

    // evenData가 HandSlot과 충돌 되었는지 확인
    private bool checkCollision(PointerEventData eventData)
    {
        GraphicRaycaster ray = GetComponentInParent<GraphicRaycaster>(); // GraphicRaycaster는 보통 Canvas에 존재

        if (ray != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current); // 이벤트 데이터 위치를 마우스 클릭 위치로 설정
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
