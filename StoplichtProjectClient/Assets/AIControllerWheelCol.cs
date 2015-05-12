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
	bool carinfront = false;
	public bool Go = true;

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
			Destroy(this.gameObject);
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
		
		RaycastHit[] allHits;
		allHits = Physics.RaycastAll(transform.position, fwd, Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude * 2, 13, 200));
		bool hitsomething = false;
		for (int i = 0; i < allHits.Length; i++) {
			RaycastHit hit = allHits[i];
			Debug.DrawLine (transform.position, hit.point, Color.cyan);
			if(hit.collider.gameObject.tag == "Car"){
				hitsomething = true;
			}
			else if(hit.collider.gameObject.tag == "Trafficlight"){
				WaitingScript controller = hit.collider.gameObject.GetComponent<WaitingScript>();
				if(controller.Go == true){
					Go = true;
				}
				else{
					Go = false;
				}
			}
			else{
				carinfront = false;
				//print("Go");
				float torque = Mathf.Clamp(distance*50, 0.0f, 1000.0f);
				//print("Giving gass");
				RightFront.motorTorque = torque;
				LeftFront.motorTorque = torque;
				RightFront.brakeTorque = 0;
				LeftFront.brakeTorque = 0;

				RightBack.motorTorque = torque;
				LeftBack.motorTorque = torque;
				RightBack.brakeTorque = 0;
				LeftBack.brakeTorque = 0;
			}
			if(Go == true){
				carinfront = false;
				//print("Go");
				float torque = Mathf.Clamp(distance*50, 0.0f, 1000.0f);
				//print("Giving gass");
				RightFront.motorTorque = torque;
				LeftFront.motorTorque = torque;
				RightFront.brakeTorque = 0;
				LeftFront.brakeTorque = 0;

				RightBack.motorTorque = torque;
				LeftBack.motorTorque = torque;
				RightBack.brakeTorque = 0;
				LeftBack.brakeTorque = 0;
			}

		}
		if(hitsomething || Go == false){
			float brakeforce = (GetComponent<Rigidbody>().velocity.magnitude * 100);
			print("BRAKE" + brakeforce);
			RightFront.motorTorque = 0;
			LeftFront.motorTorque = 0;
			RightBack.motorTorque = 0;
			LeftBack.motorTorque = 0;
			RightFront.brakeTorque = brakeforce;
			LeftFront.brakeTorque = brakeforce;
			RightBack.brakeTorque = brakeforce;
			LeftBack.brakeTorque = brakeforce;
		}
		if(allHits.Length == 0){
			float torque = Mathf.Clamp(distance*50, 0.0f, 1000.0f);
			//print("Giving gass");
			RightFront.motorTorque = torque;
			LeftFront.motorTorque = torque;
			RightFront.brakeTorque = 0;
			LeftFront.brakeTorque = 0;

			RightBack.motorTorque = torque;
			LeftBack.motorTorque = torque;
			RightBack.brakeTorque = 0;
			LeftBack.brakeTorque = 0;
		}
	
		//print("CarAngle " + carAngle);
		//print("SteeringAngle " + steering + 180);
		steering = newsteering;
		RightFront.steerAngle = steering;
		LeftFront.steerAngle = steering;
		//print(steering);
	}
}
