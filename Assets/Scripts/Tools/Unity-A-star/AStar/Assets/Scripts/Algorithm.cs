/// <summary>
/// The heart of the implementation.
/// Provides the core functionality of the A* algorithm.
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class Algorithm
{
    private Grid2D grid;

    private List<Cell> openList;
    private List<Cell> closedList;

    private Vector2 start, goal;

    private CellBlockedFunction blockedFunc;

    const int maxIterationCount = 5000;


    private Algorithm () { }

    public Algorithm (Grid2D grid, Vector2 start, Vector2 goal, CellBlockedFunction blockedFunc)
    {
        this.grid = grid;
        this.start = start;
        this.goal = goal;
        this.blockedFunc = blockedFunc;

        openList = new List<Cell>();
        closedList = new List<Cell>();
    }


    public Cell[] AStarSearch ()
    {
        openList.Clear();
        closedList.Clear();

        var startCell = grid.FindCellByPosition(start);
        var goalCell = grid.FindCellByPosition(goal);

        if (grid.FindCellByPosition(start) == grid.FindCellByPosition(goal)) return new Cell[0];

        startCell.heuristic = (goal - startCell.position).magnitude;
        openList.Add(startCell);

        int counter = 0;

        while (openList.Count > 0)
        {
            counter++;
            if(counter > maxIterationCount)
            {
                Debug.Log("exceeded max iteration limit!!!");
                return null;
            }

            var bestCell = GetBestCell();
            openList.Remove(bestCell);

            var neighbours = grid.GetMooreNeighbours(bestCell);
            for (int i = 0; i < 8; i++)
            {
                var curCell = neighbours[i];
                float additionalCost = 0;

                if (curCell == null)
                    continue;
                if (curCell == goalCell)
                {
                    curCell.parent = bestCell;
                    return ConstructPath(curCell);
                }

                if (blockedFunc(curCell.position))
                {
                    continue;
                }

                var g = bestCell.cost + (curCell.position - bestCell.position).magnitude;
                var h = (goal - curCell.position).magnitude;

                if (openList.Contains(curCell) && curCell.f < (g + h))
                    continue;
                if (closedList.Contains(curCell) && curCell.f < (g + h))
                    continue;



                curCell.cost = g;
                curCell.heuristic = h;
                curCell.parent = bestCell;

                if (!openList.Contains(curCell))
                    openList.Add(curCell);
            }

            if (!closedList.Contains(bestCell))
                closedList.Add(bestCell);
        }

        return null;
    }


    // We could shorten this with a nice linq statement. However, linq has a considerable overhead compared to classic iteration.
    private Cell GetBestCell ()
    {
        Cell result = null;
        float currentF = float.PositiveInfinity;

        for (int i = 0; i < openList.Count; i++)
        {
            var cell = openList[i];

            if (cell.f < currentF)
            {
                currentF = cell.f;
                result = cell;
            }
        }

        return result;
    }


    private Cell[] ConstructPath (Cell destination)
    {
        var path = new List<Cell>() { destination };

        var current = destination;
        int counter = 0;
        while (current.parent != null)
        {
            counter++;
            if(counter > maxIterationCount)
            {
                Debug.Log("Construct path exceeded max iteration count!");
                return null;
            }
            current = current.parent;
            path.Add(current);
        }

        path.Reverse();
        return path.ToArray();
    }
}

public delegate bool CellBlockedFunction(Vector2 position);
