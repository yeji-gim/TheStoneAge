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
    public TMP_Text dialougeText;
    [Header("button panel")]
    public Button button;
    public TMP_Text button_name;
    Queue<DialogueLine> dialogueQueue;

    bool istyping = false;
    Action onDialogueEnd = null;
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


    public void StartDialogue(List<DialogueLine> dialogueLinesToQueue)
    {

        dialogueQueue = new Queue<DialogueLine>(dialogueLinesToQueue);
        Debug.Log($"dialogueQueue.Count {dialogueQueue.Count}");
        UpdateDialogue();
    }


    public void UpdateDialogue()
    {
        if (istyping)
        {
            istyping = false;
            return;
        }
        dialougeText.text = string.Empty;
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLine line = dialogueQueue.Dequeue();


        if (!string.IsNullOrEmpty(line.button))
        {
            Debug.Log("button action");
            Talk(line.speaker,line.message, line.button);
            UIManager.Instance.TriggerDialogePrompt(line.button, OnFirstButtonClicked);
        }
        else
        {
            Talk(line.message);
        }
    }

    public void EndDialogue()
    {
        dialougePanel.SetActive(false);
        onDialogueEnd = null;
        UIManager.Instance.npcCameraOff();
    }

    public void Talk(string message)
    {
        dialougePanel.SetActive(true);
        StartCoroutine(TypeText(message));
    }
    public void Talk(string thisspeaker, string message, string f_button)
    {
        dialougePanel.SetActive(true);
        speaker = thisspeaker;
        StartCoroutine(TypeText(message));
    }
    IEnumerator TypeText(string textToType)
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
    public void OnFirstButtonClicked()
    {
        if (button_name.text.Contains("퀘스트"))
        {
            if (speaker == "mom")
            {
                GameObject momObject = GameObject.FindGameObjectWithTag("momquest");
                GameObject mom = GameObject.FindGameObjectWithTag("mom");
                QuestManager momquest = momObject.GetComponent<QuestManager>();
                momDialogue momDialogue = mom.GetComponent<momDialogue>();
                UIManager.Instance.DisplayQuest(momquest.quest[momDialogue.index]);
                HandleUI();
            }
            if (speaker == "dad")
            {
                GameObject dadObject = GameObject.FindGameObjectWithTag("dadquest");
                GameObject dad = GameObject.FindGameObjectWithTag("dad");
                QuestManager dadquest = dadObject.GetComponent<QuestManager>();
                dadDialogue dadDialogue = dad.GetComponent<dadDialogue>();
                UIManager.Instance.DisplayQuest(dadquest.quest[dadDialogue.index]);
                HandleUI();

            }
            if(speaker == "sister")
            {
                GameObject sisterOjbect = GameObject.FindGameObjectWithTag("sisterquest");
                GameObject sister = GameObject.FindGameObjectWithTag("sister");
                QuestManager sisterquest = sisterOjbect.GetComponent<QuestManager>();            
                sisterDialogue sisterDialogue = sister.GetComponent<sisterDialogue>();
                UIManager.Instance.DisplayQuest(sisterquest.quest[sisterDialogue.index]);
                HandleUI();
            }

            if (speaker == "grandfather")
            {
                GameObject grandfatherObject = GameObject.FindGameObjectWithTag("grandfatherquest");
                GameObject grandfather = GameObject.FindGameObjectWithTag("grandfather");
                QuestManager grandfatherquest = grandfather.GetComponent<QuestManager>();
                grandFatherDialogue grandfatherDialogue = grandfather.GetComponent<grandFatherDialogue>();
                UIManager.Instance.DisplayQuest(grandfatherquest.quest[grandfatherDialogue.index]);
                HandleUI();
            }
        }
        if (button_name.text.Contains("성인식"))
        {
            SceneManager.LoadScene("Ending2");
        }

    }
    public void HandleUI()
    {
        UIManager.Instance.ShowgetQuesPanel();
        UIManager.Instance.button.gameObject.SetActive(false);
        UIManager.Instance.dialoguepanel.SetActive(false);
        UIManager.Instance.npcCameraOff();
    }
}
