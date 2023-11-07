using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogueLine
{
    public string speaker;
    [TextArea(2, 5)]
    public string message;
    public string button;

    public DialogueLine(string message)
    {
        this.message = message;
    }


    public DialogueLine(string speaker,string message, string button)
    {
        this.speaker = speaker;
        this.message = message;
        this.button = button;
    }
}
