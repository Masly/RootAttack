using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class TestScriptableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (GameEvent)target;

        if (GUILayout.Button("Raise Event", GUILayout.Height(40)))
        {
            script.Raise();
        }

    }
}