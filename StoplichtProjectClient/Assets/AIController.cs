using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIController : MonoBehaviour {
	public WheelCollider RightFront;
	public WheelCollider LeftFront;
	public WheelCollider RightBack;
	public WheelCollider LeftBack;

	
	public GameObject WaypointCollection;
	public int maxspeed;
	private List<GameObject> waypoints;
	private int waypointcounter = 0;
	public GameObject Target;

	// Use this for initialization
	void Start () {
		RightBack.motorTorque = maxspeed;
		RightFront.motorTorque = maxspeed;
		LeftBack.motorTorque = maxspeed;
		LeftFront.motorTorque = maxspeed;

		waypoints = new List<GameObject>();
		foreach(Transform gameObj in WaypointCollection.transform){
			waypoints.Add(gameObj.gameObject);
		}
		Target = waypoints[waypointcounter];
	}
	public void nextWaypoint(){
		waypointcounter++;
		Target = waypoints[waypointcounter];
		}
	
	// Update is called once per frame
	void Update () {
		if(transform.position == Target.transform.position){
			waypointcounter++;
			Target = waypoints[waypointcounter];
		}
		float xDistance = Target.transform.position.x - transform.position.x;
		float zDistance = Target.transform.position.z - transform.position.z;
		float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
		float carAngle = transform.localEulerAngles.y;
		float steering = (angle - carAngle);
		//print(steering);
		//RightFront.steerAngle = Mathf.Clamp(steering+360, -70.0f, 70.0f);
		RightFront.steerAngle = steering;
		LeftFront.steerAngle = steering;
		//LeftFront.steerAngle = Mathf.Clamp(steering+360, -70.0f, 70.0f);
		//RightFront.steerAngle = -45;
		//LeftFront.steerAngle = -45;
	}
}
