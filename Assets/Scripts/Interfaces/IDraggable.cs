using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    void OnDrag(Vector3 mouseFloorPosition);
    void OnRelease();

    GameObject gameObject { get; }
}
