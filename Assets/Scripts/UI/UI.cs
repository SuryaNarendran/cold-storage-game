using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : Singleton<UI>
{
    public PopUpValue mainUiSensor;
    public GameObject victoryPopUp;
    public GameObject cursorIndicator;
    public InfoPageUI lockDoorUI;
    public InfoPageUI temperatureSourceUI;
    public InfoPageUI temperatureSourceSimpleUI;
    public InfoPageUI lockDoorSourceUI;
    public InfoPageUI mainDoorDisplayUI;
    public TMP_Text nameText;
    public MenuUI mainMenuUI;

    public static PopUpValue MainUiSensor { get => Instance.mainUiSensor; }
    public static InfoPageUI LockDoorUI { get => Instance.lockDoorUI; }
    public static InfoPageUI TemperatureSourceUI { get => Instance.temperatureSourceUI; }
    public static InfoPageUI TemperatureSourceSimpleUI { get => Instance.temperatureSourceSimpleUI; }
    public static InfoPageUI LockDoorSourceUI { get => Instance.lockDoorSourceUI; }
    public static MenuUI MainMenuUI { get => Instance.mainMenuUI; }
    public static InfoPageUI MainDoorDisplayUI { get => Instance.mainDoorDisplayUI; }

    public static ISelectable selectedObject;

    public static void VictoryMessage(bool state)
    {
        Instance.victoryPopUp.SetActive(state);
    }

    public static void CursorIndicator(bool state)
    {
        Instance.cursorIndicator.SetActive(state);
    }

    public static void SetIndicatorPosition(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, Instance.cursorIndicator.transform.position.y, position.z);
        Instance.cursorIndicator.transform.position = newPos;
    }

    public static void SetAsSelected ( ISelectable newSelection)
    {
        DeSelectExisting();
        selectedObject = newSelection;
        selectedObject.infoSheetObject.SetActive(true);
        selectedObject.UpdateInfoPage();
        Instance.nameText.text = selectedObject.displayName;
        Audiomanager.Play("kik");
        selectedObject.onSelection?.Invoke();
    }

    public static void DeSelectExisting()
    {
        if (selectedObject != null)
        {
            selectedObject.infoSheetObject.SetActive(false);
            selectedObject.onDeSelection?.Invoke();
            Instance.nameText.text = "";
            selectedObject = null;
        }
    }
}
