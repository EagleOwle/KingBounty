using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HexWorld.MapMaker))]
public class MapMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HexWorld.MapMaker generator = (HexWorld.MapMaker)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        if (GUILayout.Button("Clear"))
        {
            generator.DestroyHexField();
        }
    }
}
