using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NumManager))]
public class NumManager_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        NumManager myTarget = (NumManager)target;

        GUI.enabled = true;

        if (GUILayout.Button("Make Buttons"))
        {
            myTarget.MakeNumbers();
        }

        DrawDefaultInspector();
    }
}
