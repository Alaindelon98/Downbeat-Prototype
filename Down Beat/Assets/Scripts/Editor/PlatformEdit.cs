using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(platformScript))]
[ExecuteInEditMode]

public class PlatformEdit : Editor
{
    platformScript platform;

    void OnEnable()
    {
        platform = (platformScript)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        platformScript platform = target as platformScript;

        if (GUILayout.Button("AddPosition", GUILayout.Width(150)))
        {
            this.serializedObject.Update();

            platform.positionsList.Add(platform.transform.position);

            EditorUtility.SetDirty(target);

            this.serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("RemoveLast", GUILayout.Width(150)))
        {

            platform.positionsList.RemoveAt(platform.positionsList.Count-1);

            EditorUtility.SetDirty(target);

            
        }
        if (GUILayout.Button("ClearList", GUILayout.Width(150)))
        {

            platform.positionsList.Clear();

            EditorUtility.SetDirty(target);


        }
    }

}
