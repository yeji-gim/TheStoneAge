using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    [Header("Inventory Systems")]
    public InventorySlot[] toolSlots;
    public InventorySlot[] ItemSlots;
    public HandInventorySlot toolHandSlot;
    public GameObject InventoryPanel;
    public GameObject Infoprompt;
    public Image ItemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    [Header("Quest Systems")]
    public GameObject QuestPanel;
    public Image getQuest;
    public TMP_Text questName;
    public TMP_Text questDescription;
    public GameObject gridItemPrefab;
    public GridLayoutGroup gridLayoutGroup;
    [Header("Dialogue")]
    public DialoguePrompt DialoguePrompt;
    public Button button;
    public TMP_Text button_name;
    public GameObject dialoguepanel;
    [Header("Settings")]
    public GameObject settingPanel;
    public GameObject creditPanel;
    [Header("Making")]
    public GameObject makingPanel;
    public GameObject handAxePanel;
    public GameObject stoneAxePanel;
    public GameObject projectilePanel;
    public GameObject spearPanel;

    private void Start()
    {
        RenderInventory();
    }

    public void TriggerDialogePrompt(string name1, System.Action first)
    {
        DialoguePrompt.gameObject.SetActive(true);
        DialoguePrompt.Createbutton(name1,  first);
    }
    

    public void RenderInventory()
    {
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);

        RenderInventoryPanel(inventoryToolSlots, toolSlots);
        RenderInventoryPanel(inventoryItemSlots, ItemSlots);


        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
    }
    void RenderInventoryPanel(ItemSlotData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }
    public void ToggleInventoryPanel()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        RenderInventory();
    }
    public void DisplayItemInfo(ItemData data)
    {
        Infoprompt.SetActive(true);
        ItemImage.sprite = data.thumbnail;
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }
    public void ToggleQuestDescription()
    {
        questName.gameObject.SetActive(!questName.gameObject.activeSelf);
        questDescription.gameObject.SetActive(!questDescription.gameObject.activeSelf);
        gridLayoutGroup.gameObject.SetActive(!gridLayoutGroup.gameObject.activeSelf);
    }
    public void DisplayQuest(QuestData data)
    {
        ToggleQuestDescription();
        getQuest.gameObject.SetActive(true);
        questName.text= data.questname;
        questDescription.text = data.description;
        foreach (QuestData.RequiredItem requiredItem in data.requiredItems)
        {
            GameObject gridItem = Instantiate(gridItemPrefab, gridLayoutGroup.transform);
            Image itemImage = gridItem.GetComponentInChildren<Image>();
            TMP_Text itemCountText = gridItem.GetComponentInChildren<TMP_Text>();

            itemImage.sprite = requiredItem.item.thumbnail;
            itemCountText.text = "    X " + requiredItem.itemCount;
        }

    }
    public bool IsInventoryPanelActive()
    {
        return InventoryPanel.activeSelf;
    }
    public void exitforInfoPrompt()
    {
        Infoprompt.SetActive(!gameObject.activeSelf);
    }
    public void ToggleQuestPanel()
    {
        getQuest.gameObject.SetActive(false);
        QuestPanel.SetActive(!QuestPanel.activeSelf);
    }
    public void ToggleDialoguePanel()
    {
        dialoguepanel.SetActive(!dialoguepanel.activeSelf);
    }
    public void TogglesettingPanell()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    public void TogglecreditPanel()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }
    public void TogglemakingPanel()
    {
        makingPanel.SetActive(!makingPanel.activeSelf);
    }
    public void fornextButton()
    {
        Debug.Log("Click");
        if (DialogueManager.Instance.dialougePanel.activeSelf)
        {
            DialogueManager.Instance.UpdateDialogue();
        }
    }

    public void TogglemakeHandAxeButton()
    {
        handAxePanel.SetActive(!handAxePanel.activeSelf);
    }

    public void TogglemakestoneAxePanelButton()
    {
        stoneAxePanel.SetActive(!stoneAxePanel.activeSelf);
    }

    public void TogglemakeprojectilePanelButton()
    {
        projectilePanel.SetActive(!projectilePanel.activeSelf);
    }

    public void TogglemakespearPanelButton()
    {
        spearPanel.SetActive(!spearPanel.activeSelf);
    }

}
