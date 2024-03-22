using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button noButton;
    public Button yesButton;
    public ItemData item;

    void Start()
    {
        noButton.onClick.AddListener(DisableCanvas);
    }

    public void DisableCanvas()
    {
        Destroy(gameObject);
    }

    public void callYesButton()
    {
        bool isPanelActive = UIManager.Instance.isInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);
            bool addedToExistingStack = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Stackable(item))
                {
                    items[i].AddQuantity();
                    addedToExistingStack = true;
                    break;
                }
            }

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

            UIManager.Instance.renderInventory();
        }
    }
}
