using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TemperatureGate : MonoBehaviour
{
    [SerializeField] public float minTemperature;
    [SerializeField] public float maxTemperature;
    [SerializeField] public DoorConditions doorCondition;
    [SerializeField] Door door;
    [SerializeField] float sensorDelay = 0.05f;

    public System.Action<float> onTemperatureUpdated;
    public System.Action onOpened;
    public System.Action onClosed;

    public bool locked { get; private set; } = true;

    private float targetTemperature;
    private float currentTemperature = 0f;


    private void Start()
    {
        StartCoroutine(CheckReadingCoroutine());
    }

    protected float UpdateReading()
    {
        targetTemperature = TemperatureMethods.Temperature(transform.position);
        currentTemperature = TemperatureMethods.TemperatureLerp(targetTemperature, currentTemperature, sensorDelay);
        onTemperatureUpdated?.Invoke(currentTemperature);
        //if(popupActive) UI.SetSensorValue(temperature);

        if (CheckBlocked()) return currentTemperature;

        switch (doorCondition)
        {
            case DoorConditions.BelowTemp:
                if (Mathf.Round(currentTemperature) <= (int)maxTemperature)
                {
                    if(locked) Unlock();
                }
                else
                {
                    if(!locked) Lock();
                }
                break;

            case DoorConditions.AboveTemp:
                if (Mathf.Round(currentTemperature) >= (int)minTemperature)
                {
                    if (locked) Unlock();
                }
                else
                {
                    if (!locked) Lock();
                }
                break;

            case DoorConditions.WithinTemp:
                if (Mathf.Round(currentTemperature) >= (int)minTemperature && Mathf.Round(currentTemperature) < (int)maxTemperature)
                {
                    if (locked) Unlock();
                }
                else
                {
                    if (!locked) Lock();
                }
                break;
        }

        return currentTemperature;
    }

    protected IEnumerator CheckReadingCoroutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateReading();
        }
    }

    private void Unlock()
    {
        locked = false;
        onOpened?.Invoke();
        door.Open();
    }

    private void Lock()
    {
        locked = true;
        onClosed?.Invoke();
        door.Close();
    }

    private bool CheckBlocked()
    {
        Vector3 raystart = transform.position + transform.rotation * new Vector3(0.2f, 0f, 1f);

        //return (Physics.Raycast(raystart, transform.rotation * Vector3.right, 0.2f, -1, QueryTriggerInteraction.Ignore));

        return false; //fix the raycast
    }

}

[System.Serializable]
public enum DoorConditions
{
    BelowTemp,
    AboveTemp,
    WithinTemp
}

