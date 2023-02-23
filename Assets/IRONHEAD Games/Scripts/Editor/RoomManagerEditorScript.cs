using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for creating and joining a random room.", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
  
        if (GUILayout.Button("Join School Room"))
        {
            roomManager.OnEnteredButtonClicked_School();
        }
        
        if (GUILayout.Button("Join Outdoor Room"))
        {
            roomManager.OnEnteredButtonClicked_Outdoor();
        }
    }
}
