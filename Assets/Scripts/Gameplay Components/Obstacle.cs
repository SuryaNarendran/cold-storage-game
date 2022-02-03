using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ITemperatureObstacle, IClickable, IDraggable
{
    Vector3Int oldCoords;

    Trolley parentTrolley;

    private void Awake()
    {
        oldCoords = GridMethods.GetCoords(this, ResourceLocator.grid);

        parentTrolley = transform.parent?.GetComponent<Trolley>();
    }

    private void Update()
    {
        OnMoved();
    }


    public void OnMoved()
    {
        Vector3Int newCoords = GridMethods.GetCoords(this, ResourceLocator.grid);
        if (oldCoords != newCoords)
        {
            if(Obstacles.GetAtCoords(oldCoords) == this)
                Obstacles.Remove(oldCoords);

            Obstacles.Add(this);
            Floor.temperatureDirty = true;
            oldCoords = newCoords;
        }

    }

    public virtual void OnClick(Vector3 hitpos)
    {
        parentTrolley?.OnClick(hitpos);
    }

    public virtual void OnDrag(Vector3 floorPos)
    {
        parentTrolley?.OnDrag(floorPos);
    }

    public virtual void OnRelease()
    {
        parentTrolley?.OnRelease();
    }
}
