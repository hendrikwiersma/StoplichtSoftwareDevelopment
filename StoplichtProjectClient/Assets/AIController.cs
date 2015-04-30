using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIController : MonoBehaviour {
	public WheelColliderSource RightFront;
	public WheelColliderSource LeftFront;
	public WheelColliderSource RightBack;
	public WheelColliderSource LeftBack;

	
	public GameObject WaypointCollection;
	public int maxspeed;
	private List<GameObject> waypoints;
	private int waypointcounter = 0;
	public GameObject Target;
	private float speed = 0.1f;
	private float tParam = 0f;

	private float steering = 0.0f;

	// Use this for initialization
	void Start () {

		waypoints = new List<GameObject>();
		foreach(Transform gameObj in WaypointCollection.transform){
			waypoints.Add(gameObj.gameObject);
		}
		Target = waypoints[waypointcounter];
	}
	public void nextWaypoint(){
		if(waypointcounter < waypoints.Count-1){
			waypointcounter++;
			print(waypointcounter);
		}
		else{
			waypointcounter = 0;
		}
		Target = waypoints[waypointcounter];
		}
	
	// Update is called once per frame
	void Update () {
		float xDistance = Target.transform.position.x - transform.position.x;
		float zDistance = Target.transform.position.z - transform.position.z;
		float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
		float carAngle = transform.localEulerAngles.y;
		float newsteering = (angle - carAngle);

		float distance = Vector3.Distance (Target.transform.position, transform.position);
		//print(20.0f * GetComponent<Rigidbody>().velocity.magnitude);
		if(distance < 5.5f * GetComponent<Rigidbody>().velocity.magnitude){
			RightFront.MotorTorque = 0;
			LeftFront.MotorTorque = 0;
			RightBack.MotorTorque = 0;
			LeftBack.MotorTorque = 0;
			RightFront.BrakeTorque = 10*distance;
			LeftFront.BrakeTorque = 10*distance;
			RightBack.BrakeTorque = 10*distance;
			LeftBack.BrakeTorque = 10*distance;
		}
		else{
			RightFront.MotorTorque = distance*3;
			LeftFront.MotorTorque = distance*3;
			RightFront.BrakeTorque = 0;
			LeftFront.BrakeTorque = 0;

			RightBack.MotorTorque = distance*3;
			LeftBack.MotorTorque = distance*3;
			RightBack.BrakeTorque = 0;
			LeftBack.BrakeTorque = 0;
		}
		

		steering = Mathf.Lerp(steering, newsteering, 0.1f);
		RightFront.SteerAngle = steering;
		LeftFront.SteerAngle = steering;
	}
}
