using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialoge : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;

    public bool active = true;

    public void StartDialogue()
    {
        if (active == false) return;
        Chatbox.DisplayDialogue(dialogue);
        active = false;
    }
}
