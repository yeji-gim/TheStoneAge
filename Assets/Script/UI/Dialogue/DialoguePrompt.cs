using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour
{
    [Header("Button")]
    public GameObject button;
    public Button firstButton;
    public TMP_Text first;

    Action onfistSelected = null;

    private void Awake()
    {
        Debug.Log("Awake");


    }
    public void Createbutton(string first_text, Action firstAction)
    {
        onfistSelected = firstAction;
        first.text = first_text;
        firstButton.onClick.AddListener(() => {
            firstAction?.Invoke();
        });
    }
    public void OnFirstButtonClicked()
    {
        onfistSelected?.Invoke();
        button.SetActive(false);
    }
}
