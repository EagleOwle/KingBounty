using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapMaker))]
public class MapMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapMaker generator = (MapMaker)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        if (GUILayout.Button("Clear"))
        {
            generator.DestroyCell();
        }
    }
}
