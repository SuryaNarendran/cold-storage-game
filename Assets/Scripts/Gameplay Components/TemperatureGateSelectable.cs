using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGateSelectable : MonoBehaviour, ISelectable
{
    [SerializeField] TemperatureGate temperatureGate;
    [SerializeField] InfoPageUI infoPageUI;
    [SerializeField] string _displayName;

    public System.Action onSelection { get; set; }
    public System.Action onDeSelection { get; set; }

    bool infoSheetActive;

    public string displayName { get => _displayName; }

    public GameObject infoSheetObject { get => infoPageUI.popUp; }

    private void Awake()
    {
        if (infoPageUI == null)
        {
            infoPageUI = UI.LockDoorUI;
        }

        onSelection += OnSelection;
        onDeSelection += OnDeSelection;

        temperatureGate.onOpened += UpdateInfoPage;
        temperatureGate.onTemperatureUpdated += UpdateInfoPage;
    }

    private void Start()
    {
        //UpdateInfoPage();
    }

    public void UpdateInfoPage(float temperature)
    {
        if (UI.selectedObject != this) return;

        float conditionTemp = (temperatureGate.doorCondition == DoorConditions.AboveTemp) 
            ? temperatureGate.minTemperature : temperatureGate.maxTemperature;

        infoPageUI.SetTargetTemp(conditionTemp);

        infoPageUI.SetValue(temperature);

        if (temperatureGate.locked)
        {
            infoPageUI.Locked(true);
            infoPageUI.SetStatusText("Locked", infoPageUI.lockedColor);
        }
        else
        {
            infoPageUI.Locked(false);
            infoPageUI.SetStatusText("Unlocked", infoPageUI.unlockedColor);
        }
    }

    public void UpdateInfoPage()
    {
        UpdateInfoPage(TemperatureMethods.Temperature(transform.position));
    }



    public void Select()
    {
        UI.SetAsSelected(this);
    }


    public void OnSelection()
    {

    }

    public void OnDeSelection()
    {

    }

}
