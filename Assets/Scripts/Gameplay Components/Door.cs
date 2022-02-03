using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] public UnityEvent onOpened;
    [SerializeField] public UnityEvent onClosed;
    [SerializeField] Animator animator;
    [SerializeField] public bool startsOpen;

    const string animOpenName = "Open";
    const string animOpenedName = "Opened";
    const string animClosedName = "Closed";
    const string animCloseName = "Close";

    public DoorStates doorState { get; private set; }

    private void Start()
    {
        if (startsOpen)
        {
            doorState = DoorStates.Open;
            animator.Play(animOpenedName);
        }
        else
        {
            doorState = DoorStates.Closed;
            animator.Play(animClosedName);
        }
    }

    public virtual void Open()
    {
        if (doorState != DoorStates.Closed) return;
        doorState = DoorStates.Open;
        onOpened?.Invoke();

        if (animator != null)
        {
            animator.SetTrigger("OpenClose");
        }

    }

    public virtual void Close()
    {
        if (doorState != DoorStates.Open) return;
        doorState = DoorStates.Closed;
        onClosed?.Invoke();

        if (animator != null)
        {
            animator.SetTrigger("OpenClose");
        }
    }
}

[System.Serializable]
public enum DoorStates
{
    Open,
    Opening,
    Closed,
    Closing
}

