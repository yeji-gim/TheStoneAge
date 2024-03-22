using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Dialogue Components")]
    public GameObject dialougePanel;
    [SerializeField] private TMP_Text dialougeText;
    [Header("Button Panel")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text button_name;
    [Header("Quest")]
    public QuestData[] momQuest;
    public QuestData[] sisterQuest;
    public QuestData[] dadQuest;
    public QuestData[] grandFatherQuest;
    Queue<DialogueLine> dialogueQueue;

    bool istyping = false;
    string speaker;
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


    public void startDialogue(List<DialogueLine> dialogueLinesToQueue)
    {
        dialogueQueue = new Queue<DialogueLine>(dialogueLinesToQueue);
        updateDialogue();
    }


    public void updateDialogue()
    {
        if (istyping)
        {
            istyping = false;
            return;
        }
        dialougeText.text = string.Empty;
        if (dialogueQueue.Count == 0)
        {
            endDialogue();
            return;
        }
        DialogueLine line = dialogueQueue.Dequeue();


        if (!string.IsNullOrEmpty(line.button))
        {
            Talk(line.speaker,line.message, line.button);
            UIManager.Instance.triggerDialogePrompt(line.button, questButtonClicked);
        }
        else
        {
            Talk(line.message);
        }
    }

    public void endDialogue()
    {
        dialougePanel.SetActive(false);
        UIManager.Instance.npcCameraOff();
    }

    public void Talk(string message)
    {
        dialougePanel.SetActive(true);
        StartCoroutine(typeText(message));
    }
    public void Talk(string thisspeaker, string message, string f_button)
    {
        dialougePanel.SetActive(true);
        speaker = thisspeaker;
        StartCoroutine(typeText(message));
    }
    IEnumerator typeText(string textToType)
    {
        istyping = true;
        char[] charsToType = textToType.ToCharArray();
        for (int i = 0; i < charsToType.Length; i++)
        {
            dialougeText.text += charsToType[i];
            yield return new WaitForSeconds(0.01f);
            if (!istyping)
            {
                dialougeText.text = textToType;
                break;
            }

        }
        istyping = false;
    }
    public void questButtonClicked()
    {
        if (button_name.text.Contains("퀘스트"))
        {
            if (speaker == "mom")
            {
                GameObject mom = GameObject.FindGameObjectWithTag("mom");
                momDialogue momDialogue = mom.GetComponent<momDialogue>();
                UIManager.Instance.DisplayQuest(momQuest[momDialogue.index]);
            }
            if (speaker == "dad")
            {
                GameObject dad = GameObject.FindGameObjectWithTag("dad");
                dadDialogue dadDialogue = dad.GetComponent<dadDialogue>();
                UIManager.Instance.DisplayQuest(dadQuest[dadDialogue.index]);         
            }
            if(speaker == "sister")
            {
                GameObject sister = GameObject.FindGameObjectWithTag("sister");           
                sisterDialogue sisterDialogue = sister.GetComponent<sisterDialogue>();
                UIManager.Instance.DisplayQuest(sisterQuest[sisterDialogue.index]);
            }
            if (speaker == "grandfather")
            {
                GameObject grandfather = GameObject.FindGameObjectWithTag("grandfather");             
                grandFatherDialogue grandfatherDialogue = grandfather.GetComponent<grandFatherDialogue>();
                UIManager.Instance.DisplayQuest(grandFatherQuest[grandfatherDialogue.index]);
            }
            resetUI();
        }
        if (button_name.text.Contains("성인식"))
        {
            SceneManager.LoadScene("Ending2");
        }

    }
    public void resetUI()
    {
        UIManager.Instance.showgetQuesPanel();
        UIManager.Instance.button.gameObject.SetActive(false);
        UIManager.Instance.dialoguepanel.SetActive(false);
        UIManager.Instance.npcCameraOff();
    }
}
