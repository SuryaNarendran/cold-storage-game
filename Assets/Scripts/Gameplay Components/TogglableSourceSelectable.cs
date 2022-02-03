using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglableSourceSelectable : SimpleSourceSelectable
{
    [SerializeField] Door door;
    [HideInInspector] public bool isClosed;
    [SerializeField] bool isBroken;

    public override void UpdateInfoPage()
    {
        if (UI.selectedObject != this) return;

        infoPageUI.SetSourceTemp(source.sourceTemp);
        if (!isBroken)
        {
            infoPageUI.SetOpenCloseButtonActive(true);
            infoPageUI.SetStatusLabelActive(true);
            infoPageUI.SetStatusText(isClosed ? "Closed" : "Open", Color.white);
            infoPageUI.SetOpenCloseButtonText(isClosed ? "Open" : "Close");
        }
        else
        {
            infoPageUI.SetOpenCloseButtonActive(false);
            infoPageUI.SetStatusLabelActive(true);
            infoPageUI.SetStatusText("Broken", Color.white);
        }
    }

    protected override void Awake()
    {
        infoPageUI = UI.TemperatureSourceUI;
        isClosed = door.startsOpen == false;
    }

    protected void Start()
    {
        if (door.startsOpen && source.active == false) source.ToggleActivity();
        if (door.startsOpen == false && source.active) source.ToggleActivity();
    }

    public void OpenClose()
    {
        isClosed = !isClosed;
        if (isClosed) door.Close();
        else door.Open();
        UpdateInfoPage();
        source.ToggleActivity();
    }
}

[System.Serializable]
public enum SourceStates
{
    Open,
    Closed,
    Locked,
    Broken,
    None
}
