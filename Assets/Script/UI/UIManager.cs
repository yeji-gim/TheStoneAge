using StarterAssets;
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
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    [Header("Quest")]
    public GameObject CompleteQuest;
    public GameObject getQuestPanel;
    [Header("Stone")]
    public IncompleteItem[] incompleteStone; 
    public GameObject[] completeStone;   

    public int clickCount = 0;
    public int maxCount;


    private void Start()
    {
        renderInventory();
        assignSlotIndexes();
    }

    public void triggerDialogePrompt(string name, System.Action first)
    {
        DialoguePrompt.gameObject.SetActive(true);
        DialoguePrompt.createButton(name,  first);
    }

    public void assignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].assignIndex(i);
            ItemSlots[i].assignIndex(i);
        }
    }
    public void renderInventory()
    {
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);

        renderInventoryPanel(inventoryToolSlots, toolSlots);
        renderInventoryPanel(inventoryItemSlots, ItemSlots);


        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
    }

    void renderInventoryPanel(ItemSlotData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }
    public void ToggleInventoryPanel()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        renderInventory();
    }
    public void DisplayItemInfo(ItemData data)
    {
        Infoprompt.SetActive(true);
        ItemImage.sprite = data.thumbnail;
        itemNameText.text = data.itemName;
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

        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (QuestData.RequiredItem requiredItem in data.requiredItems)
        {
            GameObject gridItem = Instantiate(gridItemPrefab, gridLayoutGroup.transform);
            Image itemImage = gridItem.GetComponentInChildren<Image>();
            TMP_Text itemCountText = gridItem.GetComponentInChildren<TMP_Text>();

            itemImage.sprite = requiredItem.item.thumbnail;
            itemCountText.text = "    X " + requiredItem.itemCount;
        }

    }
    public bool isInventoryPanelActive()
    {
        return InventoryPanel.activeSelf;
    }

    public bool isDialoguePromptActive()
    {
        return dialoguepanel.activeSelf;
    }

    public void exitforInfoPrompt()
    {
        Infoprompt.SetActive(!gameObject.activeSelf);
    }
    public void toggleQuestPanel()
    {
        getQuest.gameObject.SetActive(false);
        QuestPanel.SetActive(!QuestPanel.activeSelf);
    }
    public void toggleDialoguePanel()
    {
        dialoguepanel.SetActive(!dialoguepanel.activeSelf);
    }
    public void togglesettingPanell()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    public void togglecreditPanel()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }
    public void togglemakingPanel()
    {
        makingPanel.SetActive(!makingPanel.activeSelf);
    }
    public void fornextButton()
    {
        if (DialogueManager.Instance.dialougePanel.activeSelf)
        {
            DialogueManager.Instance.updateDialogue();
        }
    }

    public void togglemakeHandAxeButton()
    {   
        craftManager handAxe = handAxePanel.GetComponent<craftManager>();
        handAxe.CheckItem();
        handAxePanel.SetActive(!handAxePanel.activeSelf);
    }

    public void togglemakestoneAxePanelButton()
    {
        craftManager stoneAxe = stoneAxePanel.GetComponent<craftManager>();
        stoneAxe.CheckItem();
        stoneAxePanel.SetActive(!stoneAxePanel.activeSelf);
    }

    public void togglemakeprojectilePanelButton()
    {
        craftManager projectileAxe = projectilePanel.GetComponent<craftManager>();
        projectileAxe.CheckItem();
        projectilePanel.SetActive(!projectilePanel.activeSelf);
    }

    public void togglemakespearPanelButton()
    {
        craftManager spear = spearPanel.GetComponent<craftManager>();
        spear.CheckItem();
        spearPanel.SetActive(!spearPanel.activeSelf);
    }

    public void npcCameraOff()
    {
        GameObject[] npcCameras = GameObject.FindGameObjectsWithTag("npcCamera");

        foreach (GameObject npcCamera in npcCameras)
        {
            npcCamera.SetActive(false);
        }
    }

    public void showQuestCompletionPanel()
    {
        StartCoroutine(fadeOut(CompleteQuest));
    }
    public void showgetQuesPanel()
    {
        StartCoroutine(fadeOut(getQuestPanel));
    }

    private IEnumerator fadeOut(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

}


