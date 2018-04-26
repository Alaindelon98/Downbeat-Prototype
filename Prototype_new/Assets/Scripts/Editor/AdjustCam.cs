using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenScript))]
[ExecuteInEditMode]

public class AdjustCam : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScreenScript screen = target as ScreenScript;
        if (GUILayout.Button("TakeMeasures", GUILayout.Width(150)))
        {
           
            screen.TakeMesures();
        }
        if (GUILayout.Button("CallCamera", GUILayout.Width(150)))
        {

            screen.CallCamera();
        }

    }
}