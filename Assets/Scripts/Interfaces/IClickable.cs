using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void OnClick(Vector3 mousePosition);

    GameObject gameObject { get; }
}
