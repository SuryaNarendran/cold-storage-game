using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour, ITemperatureObstacle
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
