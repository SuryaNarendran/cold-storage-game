using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TemperatureSource : MonoBehaviour, IMappable
{
    [SerializeField] public float sourceTemp;
    [SerializeField] public float baseTransferRate;
    [SerializeField] private bool _active = true;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem fx;
    
    public bool active
    {
        get { return _active; }
        private set { _active = value; }
    }


    public void ToggleActivity()
    {
        active = !active;
        if (active) fx.Play();
        else fx.Stop();
        Floor.temperatureDirty = true;
    }

    public float Distance(Vector3 targetPosition)
    {
        Vector3Int start = GridMethods.GetCoords(this, ResourceLocator.grid);
        Vector3Int end = GridMethods.GetCoords(targetPosition, ResourceLocator.grid);

        if (Vector3Int.Distance(start, end) > 12) return 13;

        else
        {
            float cost  = Pathfinding.GetPathCost(start, end);
            return cost;
        }
    }

    private void UpdateColor()
    {
        if (active)
        {
            Material material = meshRenderer.material;
            material.color = TemperatureMethods.TemperatureColor(sourceTemp);
        }
        else
        {
            Material material = meshRenderer.material;
            material.color = Color.white;
        }
    }
  
}




