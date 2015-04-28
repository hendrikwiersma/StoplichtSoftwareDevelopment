using UnityEngine;
using System.Collections;

public class waypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// Draw the waypoint pickable gizmo
	void OnDrawGizmos() {
		Gizmos.DrawIcon (transform.position, "wheel.tiff");
	}
}
