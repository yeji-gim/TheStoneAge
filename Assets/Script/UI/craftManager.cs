using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class craftManager : MonoBehaviour
{
    [SerializeField]
    private ItemData[] item;
    [SerializeField]
    private GameObject[] itemImages;
    [SerializeField]
    private Button button;
    private void Start()
    {
        button.interactable = false;
    }
    private void Update()
    {
        CheckItem();
    }
    public void CheckItem()
    {
        ItemSlotData[] items = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);
        ItemSlotData[] Equipmentitems = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Tool);
        bool AllSetActive = true;
        CheckItemArray(items);
        CheckItemArray(Equipmentitems);

        for (int i = 0; i < itemImages.Length; i++)
        {
            if (!itemImages[i].activeSelf)
            {
                AllSetActive = false;
                break;
            }
        }
        if (AllSetActive)
            button.interactable = true;
    }

    private void CheckItemArray(ItemSlotData[] items)
    {
        for (int i = 0; i < item.Length; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if (items[j].itemData != null && item[i] != null && items[j].itemData.name == item[i].name)
                {
                    itemImages[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public void BuildHandAxeClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 0;
        UIManager.Instance.makingPanel.SetActive(false);
        UIManager.Instance.handAxePanel.SetActive(false);
    }
    public void BuildStoneAxeClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 1;
        UIManager.Instance.makingPanel.SetActive(false);
        UIManager.Instance.stoneAxePanel.SetActive(false);
    }
    public void BuildArrowClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 2;
        UIManager.Instance.makingPanel.SetActive(false);
        UIManager.Instance.projectilePanel.SetActive(false);
    }
    public void BuildSpearClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 3;
        UIManager.Instance.makingPanel.SetActive(false);
        UIManager.Instance.spearPanel.SetActive(false);
    }
}
