using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridGenerator generator = (GridGenerator)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.GenerateGrid();
        }

        if (GUILayout.Button("Clear"))
        {
            generator.DestroyCell();
        }
    }
}
