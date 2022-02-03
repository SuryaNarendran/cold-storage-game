using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureMethods : Singleton<TemperatureMethods>
{

    [SerializeField] float ambientTemp;
    [SerializeField] float ambientTransferRate = 1;
    [SerializeField] Gradient tempGradient;
    [SerializeField] Gradient brightTempGradient;
    [SerializeField] float minTemp;
    [SerializeField] float maxTemp;
    [SerializeField] float lerpRate;

    public static float AmbientTemp { get => Instance.ambientTemp; }
    public static float AmbientTransferRate { get => Instance.ambientTransferRate; }

    public static float Temperature(Vector3 position)
    {
        float temperatureAggregate = AmbientTemp * AmbientTransferRate;
        float rateAggregate = AmbientTransferRate;

        foreach (TemperatureSource source in TemperatureSources.All())
        {
            if (source.active == false) continue;
            float sourceDistance = source.Distance(position);

            float rate = 0f;
            //approximates to infinity for distance of 0
            if (sourceDistance < 0.01f) rate = 1000;

            //if source is further away than 8 tiles, ignore the effect
            else if (sourceDistance > 12) continue;

            //inverse square law for temperature change over distance
            else rate = 1f / (sourceDistance*sourceDistance) * source.baseTransferRate;

            rateAggregate += rate;
            temperatureAggregate += rate * source.sourceTemp;
        }

        return temperatureAggregate / rateAggregate;
    }

    public static Color TemperatureColor(float temperature, bool bright = false)
    {
        float t = (temperature - Instance.minTemp) / (Instance.maxTemp - Instance.minTemp);
        if (!bright) return Instance.tempGradient.Evaluate(t);
        else return Instance.brightTempGradient.Evaluate(t);
    }

    public static float TemperatureLerp(float target, float current, float intervalTime)
    {
        float min = target > current ? current : target;
        float max = target > current ? target : current;
        float direction = Mathf.Sign(target - current);
        return Mathf.Clamp(current + direction * Instance.lerpRate * intervalTime, min, max);
    }


    //public void Update()
    //{
    //    Floor.ReCalculateTemperature();
    //}
}
