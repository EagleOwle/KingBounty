using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator generator = (MapGenerator)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.GenerateGrid();
        }

        if (GUILayout.Button("SetWater"))
        {
            generator.GenerateWater();
        }

        if (GUILayout.Button("Clear"))
        {
            generator.DestroyCell();
        }
    }
}
