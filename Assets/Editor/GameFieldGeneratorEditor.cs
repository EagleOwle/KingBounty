using HexWorld;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameFieldGenerator))]
public class GameFieldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if (GUILayout.Button("Create New Field"))
        {
            (target as GameFieldGenerator).CreateNewField();
        }

        if (GUILayout.Button("Clear"))
        {
            (target as GameFieldGenerator).ClearField();
        }
    }
}