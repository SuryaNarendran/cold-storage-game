using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSensor : MonoBehaviour, IHoverable
{

    [SerializeField] protected PopUpValue popUp;
    [SerializeField] protected bool permanent = false;
    protected bool popupActive = false;

    private void Awake()
    {
        popUp = UI.MainUiSensor;
    }

    private void Start()
    {
        StartCoroutine(CheckReadingCoroutine());
    }


    public void OnHover(Vector3 mousePos)
    {
        if (!permanent) popUp.PopUp();
        popupActive = true;
        UpdateReading();
    }

    public void OnHoverLost()
    {
        if (!permanent) popUp.TakeDown();
        popupActive = false;
    }

    protected IEnumerator CheckReadingCoroutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateReading();
        }
    }

    private float UpdateReading()
    {
        Vector3 mousePos;
        if(MouseCast.MousePositionOnFloor(out mousePos))
        {
            if (Floor.DimensionBounds.Contains(mousePos))
            {
                float temperature = TemperatureMethods.Temperature(mousePos);
                popUp.SetValue(temperature);

                return temperature;
            }
            else
            {
                UI.MainUiSensor.HideValue();
            }
        }
        return 0;
    }
}

