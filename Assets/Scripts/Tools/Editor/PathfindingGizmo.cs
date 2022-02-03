using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public class PathfindingGizmo : Singleton<PathfindingGizmo>
{
    public Transform start, end;
    public Vector3Int[] path = new Vector3Int[0];

    [DrawGizmo(GizmoType.Selected)]
    static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
    {
        if (Instance == null) return;

        if (Instance.path.Length > 0)
        {
            Debug.Log("Began Drawing");

            Gizmos.color = Color.green;
            Vector3Int previousPoint = Instance.path[0];
            for (int i = 1; i < Instance.path.Length; i++)
            {
                Vector3 point1 = GridMethods.GetPosition(previousPoint, ResourceLocator.grid);
                Vector3 point2 = GridMethods.GetPosition(Instance.path[i], ResourceLocator.grid);
                Gizmos.DrawLine(point1, point2);
                //Gizmos.DrawLine(previousPoint + Vector3.up, Instance.path[i] + Vector3.up);
                previousPoint = Instance.path[i];
            }
        }
    }
}

[CustomEditor(typeof(PathfindingGizmo))]
public class PathfindingGizmoEditor : Editor
{
    PathfindingGizmo pgz;
    SerializedProperty startProp, endProp;

    private void OnEnable()
    {
        startProp = serializedObject.FindProperty("start");
        endProp = serializedObject.FindProperty("end");

         pgz = (target as PathfindingGizmo);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(startProp);
        EditorGUILayout.PropertyField(endProp);

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Show"))
        {
            Debug.Log("Calculating path");

            Pathfinding.Generate(Floor.Dimensions.x, Floor.Dimensions.y);

            Vector3Int start3 = GridMethods.GetCoords(pgz.start, ResourceLocator.grid);
            Vector3Int end3 = GridMethods.GetCoords(pgz.end, ResourceLocator.grid);

            pgz.path = Pathfinding.GetPath(start3, end3);
        }
    }
}

#endif //UNITY_EDITOR
