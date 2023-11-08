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

    bool AllSetActive;


    private void Start()
    {
        button.interactable = false;
        AllSetActive = true;
    }
    public void Update()
    {
        ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
        ItemSlotData[] Equipmentitems = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);

        for (int i = 0; i < item.Length; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if (items[j].itemData != null && item[i] != null)
                {
                    if (items[j].itemData.name == item[i].name)
                    {
                        itemImages[i].gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < item.Length; i++)
        {
            for (int j = 0; j < Equipmentitems.Length; j++)
            {
                if (Equipmentitems[j].itemData != null && item[i] != null)
                {
                    if (Equipmentitems[j].itemData.name == item[i].name)
                    {
                        Debug.Log(item[i].name);
                        itemImages[i].gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < itemImages.Length; i++)
        {
            if (!itemImages[i].activeSelf)
            {
                Debug.Log("비활성화된게 있음");
                AllSetActive = false;
                break;
            }
        }
        if (AllSetActive)
        {
            button.interactable = true;
        }
    }
    public void BuildHandAxeClick()
    {
        Debug.Log("BuildHandAxeClick");
        SceneManager.LoadScene("BuildHandAxe");
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildStoneAxeClick()
    {
        SceneManager.LoadScene("BuildStoneAxe");
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildArrowClick()
    {
        SceneManager.LoadScene("BuildArrow");
        UIManager.Instance.makingPanel.SetActive(false);
    }
    public void BuildSpearClick()
    {
        SceneManager.LoadScene("BuildSpear");
        UIManager.Instance.makingPanel.SetActive(false);
    }
}
