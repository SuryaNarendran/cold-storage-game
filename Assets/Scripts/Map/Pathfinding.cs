using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : Singleton<Pathfinding>
{
    private Grid2D grid2D;
    private Algorithm algorithm;

    [SerializeField] float maxCost = 8;


    public static void Generate(int width, int length)
    {
        //pathfinding grid is one tile bigger on each side, to account for obstacle borders
        Instance.grid2D = new Grid2D(width+2, length+2);
        Instance.grid2D.Generate();
    }

    public static float GetPathCost(Vector3Int start, Vector3Int end)
    {
        Instance.grid2D.Generate();

        Cell[] path = new Algorithm
            (Instance.grid2D, 
            ConvertToPathfindingCoord(start), 
            ConvertToPathfindingCoord(end), 
            Instance.GetBlocked)
            .AStarSearch();

        if (path == null)
            return Instance.maxCost;

        if (path.Length == 0)
            return 0;

        return Instance.GetCost(path);
    }

    private static Vector2Int ConvertToPathfindingCoord(Vector3Int vector3Int)
    {
        //pathfinding grid is one tile bigger on each side, to account for obstacle borders
        return new Vector2Int(vector3Int.x+1, vector3Int.z+1);
    }

    private static Vector3Int ConvertToWorldGridCoords(Vector2Int vector2Int)
    {
        //pathfinding grid is one tile bigger on each side, to account for obstacle borders
        return new Vector3Int(vector2Int.x - 1, 0, vector2Int.y - 1);
    }

    private static Vector3Int ConvertToWorldGridCoords(Vector2 vector2)
    {
        Vector2Int vector2Int = new Vector2Int((int)vector2.x, (int)vector2.y);
        //pathfinding grid is one tile bigger on each side, to account for obstacle borders
        return new Vector3Int(vector2Int.x - 1, 0, vector2Int.y - 1);
    }

    public bool GetBlocked(Vector2 position)
    {
        Vector3Int worldCoords = ConvertToWorldGridCoords(position);
        return Obstacles.ExistsAtCoords(worldCoords);
    }

    private float GetCost(Cell[] cells)
    {
        float value = 0f;
        for(int i=1;i<cells.Length;i++)
        {
            value += Vector2.Distance(cells[i - 1].position, cells[i].position) * ResourceLocator.grid.cellSize.x;
        }

        return value;
    }

    public static Vector3Int[] GetPath(Vector3Int start, Vector3Int end)
    {
        Instance.grid2D.Generate();

        Cell[] path = new Algorithm
            (Instance.grid2D,
            ConvertToPathfindingCoord(start),
            ConvertToPathfindingCoord(end),
            Instance.GetBlocked)
            .AStarSearch();

        Vector3Int[] retval = new Vector3Int[path.Length];
        for (int i = 0; i < path.Length; i++) retval[i] = ConvertToWorldGridCoords(path[i].position);

        return retval;
    }



}
