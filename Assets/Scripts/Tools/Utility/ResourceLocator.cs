using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-1)]
public class ResourceLocator : Singleton<ResourceLocator>
{
    private Grid _grid;
    private AStarMaster _aStarMaster;

    public static Grid grid
    {
        get
        {
            if (Instance._grid == null || !Instance._grid.gameObject.activeInHierarchy)
                Instance._grid = FindObjectOfType<Grid>();
                return Instance._grid;       
        }
    }

    public static AStarMaster aStarMaster
    {
        get
        {
            if (Instance._aStarMaster == null)
                Instance._aStarMaster = FindObjectOfType<AStarMaster>();
            return Instance._aStarMaster;
        }
    }
}
