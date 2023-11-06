using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class nextDialogue : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (DialogueManager.Instance.dialougePanel.activeSelf)
        {
            DialogueManager.Instance.UpdateDialogue();
        }
    }
}
