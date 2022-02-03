using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField] public DialogueNode[] dialogueNodes;
}

[System.Serializable]
public class DialogueNode
{
    [SerializeField] [TextArea] public string content;
}
