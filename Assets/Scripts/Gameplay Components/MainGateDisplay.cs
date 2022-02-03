using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGateDisplay : MonoBehaviour
{
    InfoPageUI infoPageUI;
    TemperatureGate temperatureGate;

    private void Start()
    {
        infoPageUI = UI.MainDoorDisplayUI;
        temperatureGate = GetComponent<TemperatureGate>();

        UpdateInitial();
        temperatureGate.onTemperatureUpdated += UpdateTemperature;
        temperatureGate.onOpened += UpdateLockedStatus;
    }

    void UpdateInitial()
    {
        float conditionTemp = (temperatureGate.doorCondition == DoorConditions.AboveTemp)
        ? temperatureGate.minTemperature : temperatureGate.maxTemperature;

        infoPageUI.SetTargetTemp(conditionTemp);
    }

    void UpdateLockedStatus()
    {
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

    void UpdateTemperature(float temperature)
    {
        infoPageUI.SetValue(temperature);
    }
}
