using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button noButton;
    public Button yesButton;
    public ItemData item;

    void Start()
    {
        // noButton에 onClick 이벤트를 추가
        noButton.onClick.AddListener(DisableCanvas);
    }

    // 캔버스를 비활성화하는 함수
    public void DisableCanvas()
    {
        Debug.Log("No 버튼이 눌렸습니다!");
        // 현재 스크립트가 연결된 캔버스를 비활성화
        Destroy(gameObject);
    }

    public void callYesButton()
    {
        bool isPanelActive = UIManager.Instance.IsInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
            bool addedToExistingStack = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Stackable(item))
                {
                    // 같은 아이템이 이미 있으면 수량을 추가
                    items[i].AddQuantity();
                    addedToExistingStack = true;
                    break;
                }
            }

            // 같은 아이템이 없어서 새 슬롯에 추가하는 경우
            if (!addedToExistingStack)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemData == null)
                    {
                        items[i].itemData = item;
                        item = null;
                        break;
                    }
                }
            }

            // 인벤토리를 다시 렌더링하고 현재 게임 오브젝트를 제거합니다.
            UIManager.Instance.RenderInventory();
            //Destroy(gameObject);
        }
    }
}
