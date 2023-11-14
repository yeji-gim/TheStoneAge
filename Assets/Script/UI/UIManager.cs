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
    
    public IncompleteItem[] incompleteStone; // 미완성 돌 이미지
    public GameObject[] completeStone;   // 완성된 돌 이미지

    public int clickCount = 0;
    public int maxCount;

    //public Canvas uiCanvas; // 활성화할 UI 알림창 캔버스

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();
        // uiCanvas.enabled = false;
    }

    public void TriggerDialogePrompt(string name1, System.Action first)
    {
        DialoguePrompt.gameObject.SetActive(true);
        DialoguePrompt.Createbutton(name1,  first);
    }

    public void AssignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            ItemSlots[i].AssignIndex(i);
        }
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
    public bool IsInventoryPanelActive()
    {
        return InventoryPanel.activeSelf;
    }

    public bool IsDialoguePromptActive()
    {
        return dialoguepanel.activeSelf;
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
        craftManager handAxe = handAxePanel.GetComponent<craftManager>();
        handAxe.CheckItem();
        Debug.Log("CHECK ITEM");

        handAxePanel.SetActive(!handAxePanel.activeSelf);
    }

    public void TogglemakestoneAxePanelButton()
    {
        craftManager stoneAxe = stoneAxePanel.GetComponent<craftManager>();
        stoneAxe.CheckItem();
        stoneAxePanel.SetActive(!stoneAxePanel.activeSelf);
    }

    public void TogglemakeprojectilePanelButton()
    {
        craftManager projectileAxe = projectilePanel.GetComponent<craftManager>();
        projectileAxe.CheckItem();
        projectilePanel.SetActive(!projectilePanel.activeSelf);
    }

    public void TogglemakespearPanelButton()
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

    public void ShowQuestCompletionPanel()
    {
        StartCoroutine(FadeOut(CompleteQuest));
    }
    public void ShowgetQuesPanel()
    {
        StartCoroutine(FadeOut(getQuestPanel));
    }

    private IEnumerator FadeOut(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

}


