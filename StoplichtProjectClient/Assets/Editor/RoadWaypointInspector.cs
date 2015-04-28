using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RoadWaypoint))]
public class RoadWaypointInspector : Editor {
	
	private RoadWaypoint waypoint;
	
	// scene render update
	private void OnSceneGUI () {

		waypoint = target as RoadWaypoint;

		Transform parent = waypoint.transform.parent;
		Road r = parent.GetComponent<Road> ();
		r.onSceneGUI ();
		
	}
	
}
