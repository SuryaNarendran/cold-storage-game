using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSourceSelectable : MonoBehaviour, ISelectable
{
    [SerializeField] private string _displayName;
    [SerializeField] protected TemperatureSource source;

    public System.Action onSelection { get; set; }
    public System.Action onDeSelection { get; set; }

    protected InfoPageUI infoPageUI { get; set; }
    public GameObject infoSheetObject { get => infoPageUI.gameObject; }
    public string displayName { get => _displayName; }

    public virtual void UpdateInfoPage()
    {
        if (UI.selectedObject != this) return;

        infoPageUI.SetSourceTemp(source.sourceTemp);
    }

    protected virtual void Awake()
    {
        infoPageUI = UI.TemperatureSourceSimpleUI;
    }

    public void Select()
    {
        UI.SetAsSelected(this);
    }
}


