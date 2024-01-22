using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaterGenerator))]
public class WaterGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaterGenerator generator = (WaterGenerator)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Clear"))
        {
            generator.ClearWater();
        }
    }
}
