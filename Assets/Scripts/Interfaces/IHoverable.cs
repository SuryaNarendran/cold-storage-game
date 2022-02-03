using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
    void OnHover(Vector3 hitPos);
    void OnHoverLost();

    GameObject gameObject { get; }
}
