using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMappable
{
    Transform transform { get; }
    GameObject gameObject { get; }
}
