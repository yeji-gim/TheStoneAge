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
        if (button_name.text.Contains("Äù½ºÆ®"))
        {
            if (speaker == "mom")
            {
                GameObject momObject = GameObject.FindGameObjectWithTag("momquest");
                QuestManager momquest = momObject.GetComponent<QuestManager>();
                momquest.getQuest();
                UIManager.Instance.ToggleDialoguePanel();
            }
            if (speaker == "dad")
            {
                GameObject dadObject = GameObject.FindGameObjectWithTag("dadquest");
                QuestManager dadquest = dadObject.GetComponent<QuestManager>();
                dadquest.getQuest();
            }
            if(speaker == "sister")
            {
                GameObject sister = GameObject.FindGameObjectWithTag("sisterquest");
                QuestManager sisterquest = sister.GetComponent<QuestManager>();
                sisterquest.getQuest();
                UIManager.Instance.dialoguepanel.SetActive(false);
            }
        }

    }
}
