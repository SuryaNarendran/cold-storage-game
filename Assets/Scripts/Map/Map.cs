using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map<T> : Singleton<Map<T>> where T : class,IMappable
{
    Dictionary<Vector3Int, T> entities = new Dictionary<Vector3Int, T>();

    private List<T> allCache;
    private bool allCacheDirty = true;

    public System.Action<T> onItemAdded;

    public static T GetAtCoords(Vector3Int coords)
    {
        if (Instance.entities.ContainsKey(coords))
            return Instance.entities[coords];
        else return null;
    }

    public static bool ExistsAtCoords(Vector3Int coords)
    {
        return Instance.entities.ContainsKey(coords);
    }

    public static List<T> All()
    {
        if (Instance.allCacheDirty)
        {
            Instance.allCache = new List<T>(Instance.entities.Values);
            Instance.allCacheDirty = false;
        }
        return Instance.allCache;
    }

    //public static List<T> All()
    //{
    //    return new List<T>(Instance.entities.Values);
    //}

    public static void Add(T entity)
    {
        Vector3Int coord = GridMethods.GetCoords(entity.gameObject, ResourceLocator.grid);
        if (Instance.entities.ContainsKey(coord))
        {
            Instance.entities[coord] = entity;
        }
        else Instance.entities.Add(coord, entity);
        Instance.allCacheDirty = true;
        Instance.onItemAdded?.Invoke(entity);

    }

    public static void Remove(Vector3Int coords)
    {
        if (Instance.entities.ContainsKey(coords))
        {
            Instance.entities.Remove(coords);
            Instance.allCacheDirty = true;
        }
    }

    protected virtual void Awake()
    {
        instance = this;
        entities.Clear();

        foreach (var entity in GetComponentsInChildren<T>())
        {
            if (entity.gameObject.activeInHierarchy == false) continue;
            Add(entity);
        }
    }
}
