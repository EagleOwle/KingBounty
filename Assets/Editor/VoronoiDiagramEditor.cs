using Assets.Scripts.Voronoi;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoronoiDiagram))]
public class VoronoiDiagramEditor :  Editor
{
    public override void OnInspectorGUI()
    {
        VoronoiDiagram generator = (VoronoiDiagram)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        if (GUILayout.Button("Clear"))
        {
            generator.Clear();
        }
    }
}
