using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    string displayName { get; }
    GameObject infoSheetObject { get; }
    void UpdateInfoPage();

    System.Action onSelection { get; set; }
    System.Action onDeSelection { get; set; }

    GameObject gameObject { get; }
}
