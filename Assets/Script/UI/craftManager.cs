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
    [SerializeField]
    public Scenename scenename;
    public enum Scenename
    {
        BuildHandAxe,
        BuildStoneAxe,
        BuildArrow,
        BuildSpear
    }
    SceneLoad sceneLoader;

    
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
        ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        ItemSlotData[] Equipmentitems = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        bool AllSetActive = true;
        CheckItemArray(items);
        CheckItemArray(Equipmentitems);

        for (int i = 0; i < itemImages.Length; i++)
        {
            if (!itemImages[i].activeSelf)
            {
                //Debug.Log("비활성화된게 있음");
                AllSetActive = false;
                break;
            }
        }
        if (AllSetActive)
        {
            //Debug.Log("활성화된게 있음");
            button.interactable = true;
        }
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
        //Debug.Log("BuildHandAxeClick");
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 0;
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildStoneAxeClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 1;
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildArrowClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 2;
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildSpearClick()
    {
        SceneManager.LoadScene("BuildItem");
        StoneClick.num = 3;
        UIManager.Instance.makingPanel.SetActive(false);
    }
}
