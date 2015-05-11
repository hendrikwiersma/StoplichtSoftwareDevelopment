using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIControllerWheelCol : MonoBehaviour {
	public WheelCollider RightFront;
	public WheelCollider LeftFront;
	public WheelCollider RightBack;
	public WheelCollider LeftBack;

	
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
			print("Next Waypoint");
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

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		//fwd.y = 0;
		RaycastHit hit;
		bool carinfront = false;
		if(Physics.Raycast(transform.position, fwd, out hit, 20)){
			Debug.DrawLine (transform.position, hit.point, Color.cyan);
			if(hit.collider.gameObject.tag == "Car"){
				carinfront = true;
			}
		}
		if(carinfront){
			print("BRAKE");
			RightFront.motorTorque = 0;
			LeftFront.motorTorque = 0;
			RightBack.motorTorque = 0;
			LeftBack.motorTorque = 0;
			RightFront.brakeTorque = 300;
			LeftFront.brakeTorque = 300;
			RightBack.brakeTorque = 300;
			LeftBack.brakeTorque = 300;
		}
		else if(distance < 0.7f * GetComponent<Rigidbody>().velocity.magnitude){
			print("Brake");
			RightFront.motorTorque = 0;
			LeftFront.motorTorque = 0;
			RightBack.motorTorque = 0;
			LeftBack.motorTorque = 0;
			RightFront.brakeTorque = 3;
			LeftFront.brakeTorque = 3;
			RightBack.brakeTorque = 3;
			LeftBack.brakeTorque = 3;
		}
		else{

			float torque = Mathf.Clamp(distance*50, 0.0f, 1000.0f);
			print(torque);
			RightFront.motorTorque = torque;
			LeftFront.motorTorque = torque;
			RightFront.brakeTorque = 0;
			LeftFront.brakeTorque = 0;

			RightBack.motorTorque = torque;
			LeftBack.motorTorque = torque;
			RightBack.brakeTorque = 0;
			LeftBack.brakeTorque = 0;
		}
		

		steering = Mathf.Lerp(steering, newsteering, 0.1f);
		RightFront.steerAngle = steering;
		LeftFront.steerAngle = steering;
		//print(steering);
	}
}
