using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevellStartup : MonoBehaviour
{
    [SerializeField] UnityEvent onLevelStart;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding.Generate(Floor.Dimensions.x, Floor.Dimensions.y);
        Obstacles.GenerateWalls(Floor.Dimensions.x, Floor.Dimensions.y);
        Floor.ReCalculateTemperature();
        onLevelStart?.Invoke();
    }
}
