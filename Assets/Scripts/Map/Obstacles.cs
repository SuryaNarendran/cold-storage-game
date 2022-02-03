using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : Map<ITemperatureObstacle>
{
    //makes walls outside the specified grid area
    public static void GenerateWalls(int width, int length)
    {
        for(int i = -1; i<width+1; i++)
        {
            Add(Border(new Vector3Int(i, 0, -1)));
            Add(Border(new Vector3Int(i, 0, length)));
        }

        //adds borders length-side, corners squares already filled by previous loop
        for (int i = 0; i < length; i++)
        {
            Add(Border(new Vector3Int(-1, 0, i)));
            Add(Border(new Vector3Int(width, 0, i)));
        }
    }

    private static ITemperatureObstacle Border(Vector3Int coords)
    {
        GameObject borderGO = new GameObject("Border");
        var border = borderGO.AddComponent<Border>();
        border.transform.position = GridMethods.GetPosition(coords, ResourceLocator.grid);
        border.transform.parent = Instance.transform;
        return border;
    }
}
