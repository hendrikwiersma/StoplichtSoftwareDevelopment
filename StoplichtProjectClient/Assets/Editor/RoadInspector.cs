using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Road))]
public class RoadInspector : Editor {
	
	private Road road;
	private bool controlPressed = false;
	private bool eSpace = false;
	private bool moveControls = false;
	private GameObject waypoint = null;

	// scene render update
	private void OnSceneGUI () {

		road = target as Road;
		road.onSceneGUI ();

		//drawMeshPoints ();
		clickEvent ();
		
	}
	
	// sidebar interface update (set vector boxes)
	public override void OnInspectorGUI () {
		
		road = target as Road;
		
		EditorGUI.BeginChangeCheck();

		GUILayout.Label ("Click and hold to move waypoints");
		GUILayout.Label ("Control click to add waypoints");
		GUILayout.Label ("Shift click to remove waypoints");
		GUILayout.Label ("Spacebar + click to change angle");

	}

	// draw mesh lines (debug)
	private void drawMeshPoints() {

		List<Vector3> points = road.getMeshPoints ();

		Handles.color = Color.green;
		for (int i = 0; i < points.Count; i += 2) {
		
			Handles.DrawLine(points[i], points[i + 1]);

		}

	}

	private void clickEvent() {

		Event e = Event.current;

		if (e.control) {

			controlPressed = true;
			createWaypoint ();

		} else {

			if (controlPressed) {

				removeWaypoint();
				controlPressed = false;

			}

		}

		if (e.type == EventType.KeyDown) {

			switch(e.keyCode) {

			case KeyCode.Space: eSpace = true; break;

			}

		}

		if (e.type == EventType.KeyUp) {
			
			switch(e.keyCode) {
				
			case KeyCode.Space: eSpace = false; break;
				
			}
			
		}

		if (e.type == EventType.mouseDown && e.button == 0) {

			if (!controlPressed) {

				RaycastHit r = waypointCollision(e);
				RoadWaypoint wp = getCollidingWaypoint(r.point);

				// check if target is a waypoint
				if (wp) {

					// remove waypoint
					if (e.shift) {

						removeWaypoint(wp.gameObject);

					// update control points
					} else if (eSpace) {
					
						moveControls = true;
						waypoint = wp.gameObject;

					// update waypoint
					} else {

						waypoint = wp.gameObject;

					}

				}

			}

		}

		if (moveControls) {

			RoadWaypoint wp = waypoint.GetComponent<RoadWaypoint>();
			wp.firstControl = waypointCollision (e).point;

			if (e.type == EventType.mouseUp && e.button == 0) {

				moveControls = false;
				waypoint = null;

			}

		} else {

			updateWaypointPosition (waypointCollision (e).point);

			if (e.type == EventType.mouseUp && e.button == 0) {

				setWaypoint ();

			}

		}

		// stop drawing selectionbox
		HandleUtility.AddDefaultControl (GUIUtility.GetControlID (FocusType.Passive));

	}
	
	private void createWaypoint() {

		if (!waypoint) {

			waypoint = road.createWaypoint();

		}

	}

	private RaycastHit waypointCollision(Event e) {

		Vector2 mouseFlipped = new Vector2 (e.mousePosition.x + 1, Camera.current.pixelHeight - e.mousePosition.y + 3);
		Ray ray = Camera.current.ScreenPointToRay (mouseFlipped);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 4000)) {


			return hit;

		}

		return new RaycastHit ();
		
	}

	private RoadWaypoint getCollidingWaypoint(Vector3 location) {

		List<RoadWaypoint> wps = new List<RoadWaypoint>();
		road.transform.GetComponentsInChildren<RoadWaypoint> (wps);

		RoadWaypoint clickedWp = null;
		wps.ForEach(delegate(RoadWaypoint wp) {
			
			if (wp.contains(location)) {

				clickedWp = wp;

			}
			
		});

		return clickedWp;

	}

	private void updateWaypointPosition(Vector3 location) {

		if (waypoint) {

			waypoint.transform.position = location;
			
		}

	}

	private void setWaypoint() {

		waypoint = null;

	}

	private void removeWaypoint() {

		removeWaypoint(waypoint);
		
	}

	private void removeWaypoint(GameObject wp) {

		if (wp) { 

			DestroyImmediate (wp);

		}
		
	}

}
