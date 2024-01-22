using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Map map = (Map)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Create First Cell"))
        {
            map.CreateFirstCell();
        }

        if (GUILayout.Button("Clear"))
        {
            //generator.DestroyCell();
        }
    }
}
