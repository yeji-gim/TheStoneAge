using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private GameObject button;
    [SerializeField] private Button questButton;
    [SerializeField] private TMP_Text buttonInfo;

    Action onQuestSelected = null;

    public void createButton(string button_text, Action buttonAction)
    {
        onQuestSelected = buttonAction;
        buttonInfo.text = button_text;
        questButton.onClick.AddListener(() => {
            buttonAction?.Invoke();
        });
    }
    public void onQuestButtonClicked()
    {
        onQuestSelected?.Invoke();
        button.SetActive(false);
    }
}
