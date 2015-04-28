// MyScriptEditor.cs
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MyScript))]
[CanEditMultipleObjects]
public class MyScriptEditor : Editor {

        SerializedProperty valueProperty;

        void OnEnable() {
                // Setup serialized property
                valueProperty = serializedObject.FindProperty("myValue");
        }

        public override void OnInspectorGUI() {
                // Update the serializedProperty
                // always do this at the start of OnInspectorGUI
                serializedObject.Update();
                EditorGUILayout.IntSlider(valueProperty, 1, 10, new GUIContent("My Value"));
                serializedObject.ApplyModifiedProperties();
        }

}