using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpValue : MonoBehaviour
{
    [SerializeField] public GameObject popUp;
    [SerializeField] TMP_Text valueText;

    public void PopUp()
    {
        popUp.SetActive(true);
    }

    public void TakeDown()
    {
        popUp.SetActive(false);
    }

    public void SetValue(float value)
    {
        //valueText.text = value.ToString("0.0") + "°C";
        valueText.text = value.ToString("0") + "°";
        valueText.color = TemperatureMethods.TemperatureColor(value*3, true);
    }

    public void HideValue()
    {
        valueText.text = "";
    }

    public string GetValue()
    {
        return valueText.text;
    }

}
