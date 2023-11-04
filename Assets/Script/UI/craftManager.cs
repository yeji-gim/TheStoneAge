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
        HandAxe,
        StoneAxe,
        Projectile,
        Spear
    }
    SceneLoad sceneLoader;

    bool AllSetActive;


    private void Start()
    {
        button.interactable = false;
        AllSetActive = true;
    }
    void Update()
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

        for(int i = 0; i<itemImages.Length;i++)
        {
            if (!itemImages[i].activeSelf)
            {
                AllSetActive = false;
                break; // 하나의 비활성화된 요소가 있으면 루프를 빠져나갑니다.
            }
        }
        if (AllSetActive)
        {
            button.interactable = true;
        }
    }

    public void LoadSceneOnClick()
    {
        sceneLoader.LoadSceneByEnum(scenename);
    }
}
