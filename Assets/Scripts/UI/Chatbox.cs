using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatbox : Singleton<Chatbox>
{
    [SerializeField] TextBox box;
    [SerializeField] float dialogueGap = 2f;

    Dialogue activeDialogue;
    Coroutine dialogueCoroutine;

    private void Start()
    {
        Instance.box.onLetterPrinted.AddListener(() => Audiomanager.Play("TextBox"));
    }

    public static void DisplayDialogue(Dialogue dialogue)
    {
        Instance.activeDialogue = dialogue;

        if (Instance.dialogueCoroutine != null)
        {
            Instance.StopCoroutine(Instance.dialogueCoroutine);
            Instance.box.ClearText();
        }
        Instance.dialogueCoroutine = Instance.StartCoroutine(Instance.DialogueSerial());
    }

    IEnumerator DialogueSerial()
    {

        for(int index = 0; index < activeDialogue.dialogueNodes.Length; index++)
        {
            box.DisplayText(activeDialogue.dialogueNodes[index].content);
            while (box.isScrollingText)
            {
                yield return null;
            }
            yield return new WaitForSeconds(dialogueGap);
        }
        dialogueCoroutine = null;
    }

    public static void Clear()
    {
        Instance.box.ClearText();
    }
}
