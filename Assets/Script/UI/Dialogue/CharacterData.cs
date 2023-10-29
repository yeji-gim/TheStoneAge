using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Charcter/Charcter")]
public class CharacterData : ScriptableObject
{
    [Header("Dialouge")]
    public List<DialogueLine> dialogueLines;
    public List<DialogueLine> noncompletedialogueLines;
    public List<DialogueLine> completedialogueLines;
}
