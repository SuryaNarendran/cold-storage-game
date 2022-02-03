using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Floor : Singleton<Floor>
{
    [SerializeField] Vector2Int dimensions;
    [SerializeField] GameObject floorTilePrefab;
    [SerializeField] Transform physicsFloor;

    const int physicsFloorSizeFactor = 10;

    Dictionary<Vector3Int, FloorTile> tiles = new Dictionary<Vector3Int, FloorTile>();

    public static Vector2Int Dimensions { get => Instance.dimensions; }
    public static Bounds DimensionBounds { get => new Bounds(Centre, Size); }
    public static Vector3 Centre
    {
        get => new Vector3(Dimensions.x / 2f, 0.5f, Dimensions.y / 2f) + ResourceLocator.grid.transform.localPosition;
    }
    public static Vector3 Size { get => new Vector3(Dimensions.x, 1f, Dimensions.y); }

    public static bool temperatureDirty = false;

    public static void Generate()
    {
        Instance._Generate();
    }

    private void _Generate()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        tiles.Clear();

        for (int i = 0; i < dimensions.x; i++)
        {
            for(int j = 0; j < dimensions.y; j++)
            {
                var floorTileGO = Instantiate(floorTilePrefab, transform);
                floorTileGO.transform.position = new Vector3(i, 0, j) * ResourceLocator.grid.cellSize.x 
                    + new Vector3(ResourceLocator.grid.cellSize.x / 2, 0, ResourceLocator.grid.cellSize.z / 2);

                tiles.Add(new Vector3Int(i, 0, j), floorTileGO.GetComponent<FloorTile>());
            }
        }

        physicsFloor.localScale =
           new Vector3(dimensions.x, 1, dimensions.y) * ResourceLocator.grid.cellSize.x / physicsFloorSizeFactor;
    }

    public static FloorTile GetAtCoords(Vector3Int coords)
    {
        if (Instance.tiles.ContainsKey(coords))
            return Instance.tiles[coords];
        else return null;
    }

    public static void ReCalculateTemperature()
    {
        foreach(FloorTile tile in Instance.tiles.Values)
        {
            tile.ResetTemperature();
        }
    }

    private void Awake()
    {
        instance = this;
        tiles.Clear();
        foreach (var tile in GetComponentsInChildren<FloorTile>())
        {
            tiles.Add(GridMethods.GetCoords(tile, ResourceLocator.grid), tile);
        }
    }

    private void LateUpdate()
    {
        if (temperatureDirty)
        {
            ReCalculateTemperature();
            temperatureDirty = false;
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(Floor))]
public class FloorCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button(new GUIContent("Generate")))
        {
            Floor.Generate();
            EditorUtility.SetDirty(target);
        }
    }
}

#endif
