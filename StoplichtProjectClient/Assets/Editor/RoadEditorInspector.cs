using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RoadEditor))]
public class RoadEditorInspector : Editor {

	private RoadEditor roadEditor;

	// scene render update
	private void OnSceneGUI () {



	}

	// sidebar interface update (set vector boxes)
	public override void OnInspectorGUI () {

		roadEditor = target as RoadEditor;

		EditorGUI.BeginChangeCheck();

		if (GUILayout.Button("Add Road")) {

			roadEditor.transform.position = new Vector3(0, .15f, 0);

			GameObject road = new GameObject("Road");
			road.transform.parent = roadEditor.transform;
			road.AddComponent<Road>();
			road.transform.position = roadEditor.transform.position;

			GameObject wp ;
			Road r = road.GetComponent<Road>();
			wp = r.createWaypoint();
			wp.transform.position = road.transform.position + new Vector3 (4, 0, 0);

			//wp = r.createWaypoint();
			//wp.transform.position = road.transform.position + new Vector3 (4, 0, 0);

		}

	}

}
