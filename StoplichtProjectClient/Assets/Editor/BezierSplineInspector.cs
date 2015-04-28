using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

	private const int lineSteps = 10;
	private const int stepsPerCurve = 10;
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private const int roadWidth = 10;
	
	private int selectedIndex = -1;
	private BezierSpline spline;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private static Color[] modeColors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};

	// scene render update
	private void OnSceneGUI () {
		spline = target as BezierSpline;
		handleTransform = spline.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		
		Vector3 p0 = ShowPoint(0);
		for (int i = 1; i < spline.ControlPointCount; i += 3) {
			Vector3 p1 = ShowPoint(i);
			Vector3 p2 = ShowPoint(i + 1);
			Vector3 p3 = ShowPoint(i + 2);
			
			Handles.color = Color.gray;
			Handles.DrawLine(p0, p1);
			Handles.DrawLine(p2, p3);
			
			Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
			p0 = p3;
		}

		ShowDirections ();
		clickEvent ();
	}

	private void ShowDirections () {

		int steps = stepsPerCurve * spline.CurveCount;

		for (int i = 0; i <= steps; i++) {

			float splineIndex = i / (float)steps;

			Vector3 point = spline.GetPoint(splineIndex);
			Vector3 direction = (spline.GetDirection(splineIndex));
			Vector3 roadEdge = Quaternion.Euler(0, 90, 0) * direction;

			Handles.color = Color.red;
			Handles.DrawLine(point, point + roadEdge * roadWidth);
			Handles.color = Color.green;
			Handles.DrawLine(point, point - roadEdge * roadWidth);
		
		}

	}

	// show points + handles
	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));
		float size = HandleUtility.GetHandleSize(point);
		if (index == 0) {
			size *= 2f;
		}
		Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
		if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)) {
			selectedIndex = index;
			Repaint();
		}
		if (selectedIndex == index) {
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(spline, "Move Point");
				EditorUtility.SetDirty(spline);
				spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
			}
		}
		return point;
	}

	// sidebar interface update (set vector boxes)
	public override void OnInspectorGUI () {
		spline = target as BezierSpline;
		EditorGUI.BeginChangeCheck();
		bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Toggle Loop");
			EditorUtility.SetDirty(spline);
			spline.Loop = loop;
		}
		if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount) {
			DrawSelectedPointInspector();
		}
		if (GUILayout.Button("Add Curve")) {
			Undo.RecordObject(spline, "Add Curve");
			spline.AddCurve();
			EditorUtility.SetDirty(spline);
		}
	}

	// handel for selected point
	private void DrawSelectedPointInspector() {
		GUILayout.Label("Selected Point");
		EditorGUI.BeginChangeCheck();
		Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Move Point");
			EditorUtility.SetDirty(spline);
			spline.SetControlPoint(selectedIndex, point);
		}
		EditorGUI.BeginChangeCheck();
		BezierControlPointMode mode = (BezierControlPointMode)
			EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(spline, "Change Point Mode");
			spline.SetControlPointMode(selectedIndex, mode);
			EditorUtility.SetDirty(spline);
		}
	}
	
	private void clickEvent() {

		Event e = Event.current;

		if (e.control) {

			if (e.isMouse && e.type == EventType.MouseDown) {

				if (e.button == 0 && e.control == true) {

					Vector2 mouseFlipped = new Vector2 (e.mousePosition.x + 1, Camera.current.pixelHeight - e.mousePosition.y + 3);
					Ray ray = Camera.current.ScreenPointToRay (mouseFlipped);
					RaycastHit hit;

					if (Physics.Raycast (ray, out hit, 4000)) {
				
						// (debug) draw point on screen
						Vector3 p = hit.point;
						Debug.DrawLine (p, new Vector3 (p.x, p.y + 20, p.z), Color.red);

						addNode (hit.point);

					}

				}

			} 

			// keep item selected on control left click
			Selection.activeGameObject = spline.transform.gameObject;

		}

	}

	private void addNode(Vector3 NodeLocation) {


	}

}