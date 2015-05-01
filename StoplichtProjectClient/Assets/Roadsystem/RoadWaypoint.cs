﻿using UnityEngine;
using System.Collections;

public class RoadWaypoint : MonoBehaviour {
	void Start(){
//		print("Should get here.");
		Instantiate(Resources.Load("waypointcollider"), transform.position, transform.rotation);
	}
	[SerializeField]
	private Vector3 _control;

	public Vector3 firstControl { 
		get { return transform.position +  _control; }
		set { _control = value - transform.position; } 
	}

	public Vector3 secondControl { 
		
		get{return transform.position - _control;} 
		
	}

	public RoadWaypoint(Vector3 controlPoint) {

		firstControl = controlPoint;

	}

	public void OnDrawGizmos()	{

		// bugged inialization outside code
		Vector3 gizmoSize = new Vector3 (3, 3, 3);
		Vector3 controlSize = new Vector3 (1, 1, 1);

		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, gizmoSize);

		Gizmos.color = Color.gray;
		Gizmos.DrawCube(firstControl, controlSize);
		Gizmos.DrawCube(secondControl, controlSize);

	}

	public bool contains(Vector3 location) {

		Vector3 v = (location - transform.position) / 6;
		v = new Vector3 (Mathf.Round(v.x),Mathf.Round(v.y),Mathf.Round(v.z));

		return v.Equals(Vector3.zero);

	}

}