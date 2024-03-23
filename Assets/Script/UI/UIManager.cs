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
    public InventorySlot[] toolSlots; // 장비창 슬롯들
    public InventorySlot[] ItemSlots; // 아이템창 슬롯들
    public HandInventorySlot toolHandSlot; // 장비 장착 슬롯
    public GameObject InventoryPanel;
    [SerializeField] private GameObject Infoprompt; // 장비 및 아이템 설명
    [SerializeField] private Image ItemImage; // 설명에 필요한 이미지
    [SerializeField] private TMP_Text itemNameText; // 설명에 필요한 이름
    [SerializeField] private TMP_Text itemDescriptionText; //설명에 필요한 Description
    [Header("Quest Systems")]
    [SerializeField] private GameObject QuestPanel;
    [SerializeField] private Image getQuest; // 
    [SerializeField] private TMP_Text questName;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [Header("Dialogue")]
    public DialoguePrompt DialoguePrompt;
    public Button button;
    public TMP_Text button_name;
    public GameObject dialoguepanel;
    [Header("Settings")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
    [Header("Making")]
    public GameObject makingPanel;
    public GameObject handAxePanel;
    public GameObject stoneAxePanel;
    public GameObject projectilePanel;
    public GameObject spearPanel;
    [Header("Quest")]
    [SerializeField] private GameObject CompleteQuest; // 퀘스트 완료 알림 ui
    [SerializeField] private GameObject getQuestPanel; // 퀘스트 생김 알림 ui
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

    // slot에 index 설정
    public void assignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].assignIndex(i);
            ItemSlots[i].assignIndex(i);
        }
    }

    // 인벤토리 업데이트
    public void renderInventory()
    {
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Item);

        renderInventoryPanel(inventoryToolSlots, toolSlots);
        renderInventoryPanel(inventoryItemSlots, ItemSlots);

        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
    }

    // 인벤토리 슬롯 정보들 업데이트
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
    // 슬롯 설명 설정
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

    // Quest 내용 설정
    public void DisplayQuest(QuestData data)
    {
        ToggleQuestDescription();
        getQuest.gameObject.SetActive(true);
        questName.text= data.questname;
        questDescription.text = data.description;

        // gridLayoutGroup 초기화
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // 필요한 아이템 gridLayout에 추가
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

    // 1초 깜빡 효과
    private IEnumerator fadeOut(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

}


