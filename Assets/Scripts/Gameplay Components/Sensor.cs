using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour, IHoverable, IMappable
{
    [SerializeField] protected PopUpValue popUp;
    [SerializeField] protected bool permanent = false;
    protected bool popupActive = false;

    public virtual void OnHover(Vector3 mousePos)
    {
        if(!permanent) popUp.PopUp();
        popupActive = true;
        UpdateReading();
    }

    public virtual void OnHoverLost()
    {
        if(!permanent) popUp.TakeDown();
        popupActive = false;
    }

    private void Update()
    {
        //if (popupActive) UpdateReading();
    }

    protected virtual float UpdateReading()
    {
        float temperature = TemperatureMethods.Temperature(transform.position);
        popUp.SetValue(temperature);
        return temperature;
    }

    protected IEnumerator CheckReadingCoroutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateReading();
        }
    }

}
