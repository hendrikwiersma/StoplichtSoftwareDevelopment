#if UNITY_EDITOR 
	using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Collections.Generic;

public class Road : MonoBehaviour {
	public int number = 0;
	private const int stepsPerCurve = 8;
	private const int roadWidth = 5;

	Vector3 vecNull = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
	
	public int WaypointCount {
		
		get { return transform.childCount; }
		
	}

	public Transform getWaypoint(int index) {

		if (WaypointCount > index && index >= 0) {

			return transform.GetChild (index);

		}

		return null;

	}

	public void Start() {
	
		MeshFilter filter = GetComponent<MeshFilter> ();

		if (!filter) {

			gameObject.AddComponent<MeshFilter> ();
			gameObject.AddComponent<MeshRenderer> ();
			filter = GetComponent<MeshFilter> ();

		}

		//Mesh mesh = filter.mesh;
		//createMesh (mesh);
		//setMaterial ();
	
	}

	public void onSceneGUI() {
		#if UNITY_EDITOR
		List<RoadWaypoint> wps = new List<RoadWaypoint>();
		transform.GetComponentsInChildren<RoadWaypoint> (wps);

		// dont draw if there are not enough waypoints
		if (wps.Count < 2) {

			return;

		}

		RoadWaypoint prev = wps [0];
		RoadWaypoint curr = wps [1];	

		for (int i = 1; i < wps.Count; i++) {

			curr = wps [i];
			Vector3 pCurr = curr.transform.position;
			Vector3 pPrev = prev.transform.position;

			if (!prev.Equals (vecNull)) {

				Handles.color = Color.gray;
				Handles.DrawLine (pCurr, curr.firstControl);
				Handles.DrawLine (pCurr, curr.secondControl);

				Handles.color = Color.green; 
				Handles.DrawBezier (pPrev, pCurr, prev.secondControl, curr.firstControl, Color.blue, null, 2);

			}

			prev = curr;

		}
		#endif
	}

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = WaypointCount - 2;
		}
		else {
			t = Mathf.Clamp01(t) * (WaypointCount - 1);
			i = (int)t;
			t -= i;
		}

		Transform start = getWaypoint (i);
		Transform end = getWaypoint (i + 1);

		// return zero vector if waypoint not found
		if ( !start || !end) {

			return Vector3.zero;

		}

		Vector3 startControl = start.GetComponent<RoadWaypoint> ().secondControl;
		Vector3 endControl = end.GetComponent<RoadWaypoint> ().firstControl;

		return transform.TransformPoint(Bezier.GetPoint(
			start.position, startControl, endControl, end.position, t)) - transform.position;
	}
	
	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = WaypointCount - 2;
		}
		else {
			t = Mathf.Clamp01(t) * (WaypointCount - 1);
			i = (int)t;
			t -= i;
		}

		Transform start = getWaypoint (i);
		Transform end = getWaypoint (i + 1);

		// return zero vector if waypoint not found
		if ( !start || !end) {
			
			return Vector3.zero;
			
		}

		Vector3 startControl = start.GetComponent<RoadWaypoint> ().secondControl;
		Vector3 endControl = end.GetComponent<RoadWaypoint> ().firstControl;

		return transform.TransformPoint(Bezier.GetFirstDerivative(
			start.position, startControl, endControl, end.position, t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public GameObject createWaypoint() {

		GameObject waypoint = new GameObject ("Waypoint");
		waypoint.AddComponent<RoadWaypoint> ();
		waypoint.transform.parent = transform;

		return waypoint;

	}

	private void createMesh(Mesh mesh) {

		List<Vector3> points = getMeshPoints ();
		List<int> triangles = new List<int>();

		for (int i = 0; i < points.Count - 2; i+=2) {

			triangles.Add(i);
			triangles.Add(i + 1);
			triangles.Add(i + 2);

			triangles.Add(i + 2);
			triangles.Add(i + 1);
			triangles.Add(i + 3);

		}

		mesh.Clear();
		mesh.vertices = points.ToArray ();
		//mesh.uv = new Vector2[]{new Vector2 (0, 0), new Vector2 (0, 100), new Vector2 (100, 100)};
		mesh.triangles = triangles.ToArray ();

		mesh.RecalculateNormals();
		mesh.Optimize ();

	}

	private void setMaterial() {

		Debug.Log ("setMat");
		//Material mat = Resources.Load("asphalt_texture", typeof(Material)) as Material;
		gameObject.GetComponent<Renderer> ().material.SetTexture(1, Resources.Load("asphalt_texture", typeof(Texture3D)) as Texture3D);


		Renderer r = gameObject.GetComponent<Renderer> ();
		print ("ArrayLen: " +r.materials.Length + " -" +r.material.name);

	}

	public List<Vector3> getMeshPoints () {
		
		List<Vector3> pointList = new List<Vector3> ();
		int steps = stepsPerCurve * WaypointCount;
		
		for (int i = 0; i <= steps; i++) {
			
			// get offset on spline
			float splineIndex = i / (float)steps;
			
			// get point on spline
			Vector3 point = GetPoint(splineIndex);
			
			// find outer edge of point
			Vector3 direction = (GetDirection(splineIndex));
			Vector3 roadEdge = Quaternion.Euler(0, 90, 0) * direction;
			
			// add point to list for both sides
			pointList.Add(point + roadEdge * roadWidth);
			pointList.Add(point - roadEdge * roadWidth);
			
		}
		
		return pointList;
		
	}

}
