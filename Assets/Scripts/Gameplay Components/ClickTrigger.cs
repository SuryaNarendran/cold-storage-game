using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickTrigger : MonoBehaviour, IClickable
{
    [SerializeField] UnityEvent onClicked;
    [SerializeField] public bool active = true;

    public void OnClick(Vector3 hitpoint)
    {
        if (active)
        {
            onClicked?.Invoke();
        }
    }
}
