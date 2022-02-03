using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridMethods
{
    public static Vector3Int GetCoords(Transform transform, Grid grid)
    {
        return GetCoords(transform.position, grid);
    }

    public static Vector3Int GetCoords(MonoBehaviour mono, Grid grid)
    {
        return GetCoords(mono.transform, grid);
    }
    public static Vector3Int GetCoords(GameObject gameObject, Grid grid)
    {
        return GetCoords(gameObject.transform, grid);
    }

    public static Vector3Int GetCoords(Vector3 position, Grid grid)
    {
        position.Scale(new Vector3(1, 0, 1));
        return grid.WorldToCell(position);
    }

    public static Vector3 GetPosition(Vector3Int coords, Grid grid)
    {
        return grid.CellToWorld(coords) + grid.cellSize/2;
    }

    public static CardinalDirection GetDirection(Vector2Int vector2Int)
    {
        if(Mathf.Abs(vector2Int.x) > Mathf.Abs(vector2Int.y))
        {
            return (vector2Int.x >= 0) ? CardinalDirection.east : CardinalDirection.west;
        }
        else
        {
            return (vector2Int.y >= 0) ? CardinalDirection.north : CardinalDirection.south;
        }
    }
}
