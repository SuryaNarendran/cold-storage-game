using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{

    private Material material;

    private float targetTemperature;
    private float currentTemperature = 0f;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void ResetTemperature()
    {
        targetTemperature = TemperatureMethods.Temperature(transform.position);
    }

    private void Update()
    {
        //if (Mathf.Abs(currentTemperature - targetTemperature) < 0.1f) return;

        currentTemperature = TemperatureMethods.TemperatureLerp(targetTemperature, currentTemperature, Time.deltaTime);
        Color newColor = TemperatureMethods.TemperatureColor(currentTemperature);
        material.color = newColor;
    }
}
