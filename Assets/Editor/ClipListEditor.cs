using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
[CustomEditor(typeof(ClipList))]
public class ClipListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ClipList clipList = target as ClipList;

        clipList.randomPitch = GUILayout.Toggle(clipList.randomPitch, "Random Pitch");

        if (clipList.randomPitch)
        {
            EditorGUILayout.LabelField("Between 0 and 3", EditorStyles.boldLabel);
            clipList.minPitch = EditorGUILayout.FloatField("Min Pitch", clipList.minPitch);
            clipList.maxPitch = EditorGUILayout.FloatField("Max Pitch", clipList.maxPitch);
        }

        clipList.randomVolume = GUILayout.Toggle(clipList.randomVolume, "Random Volume");

        if (clipList.randomVolume)
        {
            EditorGUILayout.LabelField("Between 0 and 1", EditorStyles.boldLabel);
            clipList.minVolume = EditorGUILayout.FloatField("Min Volume", clipList.minVolume);
            clipList.maxVolume = EditorGUILayout.FloatField("Max Volume", clipList.maxVolume);
        }
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(clipList);
            EditorSceneManager.MarkSceneDirty(clipList.gameObject.scene);
        }
    }
}