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

    // 버튼 생성 및 초기화
    public void createButton(string button_text, Action buttonAction)
    {
        onQuestSelected = buttonAction;
        buttonInfo.text = button_text;
        questButton.onClick.AddListener(() => { buttonAction?.Invoke();});
    }

    // 버튼이 클릭되었을때 호출
    public void onQuestButtonClicked()
    {
        onQuestSelected?.Invoke();
        button.SetActive(false);
    }
}
