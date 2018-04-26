using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenScript))]
[ExecuteInEditMode]

public class AdjustCam : Editor
{
    ScreenScript screen;

    void OnEnable()
    {
        screen = (ScreenScript)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScreenScript screen = target as ScreenScript;
       
        if (GUILayout.Button("TakeMeasures", GUILayout.Width(150)))
        {
            this.serializedObject.Update();
           
            screen.TakeMesures();

            EditorUtility.SetDirty(target);

            this.serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("CallCamera", GUILayout.Width(150)))
        {

            screen.CallCamera();
        }

    }
}