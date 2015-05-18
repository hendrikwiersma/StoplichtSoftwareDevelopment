using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class AIControllerWheelCol : MonoBehaviour {
	
	public WheelCollider RightFront;
	public WheelCollider LeftFront;
	public WheelCollider RightBack;
	public WheelCollider LeftBack;

	
	public GameObject WaypointCollection;
	public GameObject WaypointCollection2;
	private List<GameObject> waypoints;
	private List<GameObject> waypoints2;
	private List<GameObject> currentwaypoints;
	private int waypointcounter = 0;
	private GameObject Target;

	private float steering = 0.0f;
	private float braketorque = 0.0f;
	private float motortorque = 0.0f;
	public bool go = true;

	// Use this for initialization
	void Start () {

		waypoints = new List<GameObject>();
		foreach(Transform gameObj in WaypointCollection.transform){
			waypoints.Add(gameObj.gameObject);
		}
		waypoints2 = new List<GameObject>();
		foreach(Transform gameObj in WaypointCollection2.transform){
			waypoints2.Add(gameObj.gameObject);
		}
		currentwaypoints = waypoints;
		Target = currentwaypoints[waypointcounter];
	}
	public void nextWaypoint(){
		if(waypointcounter < currentwaypoints.Count-1){
			waypointcounter++;
		}
		else{
			Destroy(this.gameObject);
		}
		Target = currentwaypoints[waypointcounter];
		}
	
	// Update is called once per frame
	void Update () {
		float xDistance = Target.transform.position.x - transform.position.x;
		float zDistance = Target.transform.position.z - transform.position.z;
		float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
		float carAngle = transform.localEulerAngles.y;
		float newsteering = (angle - carAngle);
		float distance = Vector3.Distance (Target.transform.position, transform.position);

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 frl = transform.TransformDirection(new Vector3(-0.3f, 0, 1));
		Vector3 frr = transform.TransformDirection(new Vector3(0.3f, 0, 1));
		
		RaycastHit[] allHits;
		RaycastHit[] leftHits = null;
		allHits = Physics.RaycastAll(transform.position, fwd, Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude * 3, 20, 200));
		bool combinehits = false;
		bool carinfront = false;
		bool cartoleft = false;
		for (int i = 0; i < allHits.Length; i++) {

			RaycastHit hit = allHits[i];
			Debug.DrawLine (transform.position, hit.point, Color.cyan);
			if(hit.collider.gameObject.tag == "Car"){
				carinfront = true;
				if(combinehits == false){
					combinehits = true;
					print("Adding left side.");
					leftHits = Physics.RaycastAll(transform.position, frr, Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude * 2, 13, 2000));
					Vector3 forward = frl *  Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude * 2, 13, 2000);
        			Debug.DrawRay(transform.position, forward, Color.green);

				}
			}
			else if(hit.collider.gameObject.tag == "Trafficlight"){
				WaitingScript controller = hit.collider.gameObject.GetComponent<WaitingScript>();
				if(controller.Go == true){
					go = true;
				}
				else{
					go = false;
				}
			}
			else{
				drive(distance);
			}
			if(go == true){
				drive(distance);
			}

		}
		if(combinehits){
			for (int i = 0; i < leftHits.Length; i++) {
				RaycastHit hit = allHits[i];
				Debug.DrawLine (transform.position, hit.point, Color.cyan);
				if(hit.collider.gameObject.tag == "Car"){
					cartoleft = true;
				}
			}
		}

		if(carinfront || go == false){
			brake();
		}
		if(allHits.Length == 0){
			drive(distance);
		}
		if(carinfront && cartoleft == false){
			RaycastHit hitleft;
			if(Physics.Raycast(transform.position, Vector3.left, out hitleft)){
				if(hitleft.collider.gameObject.tag != "Car"){
					currentwaypoints = waypoints2;
					//nextWaypoint();
					//waypointcounter = waypointcounter -1;
				}
			}
			else{
				currentwaypoints = waypoints2;
				//nextWaypoint();
			}
			RaycastHit hitright;
			if(Physics.Raycast(transform.position, Vector3.right, out hitright, 2.0f) && hitright.collider.gameObject.tag != "Car"){
				//Debug.DrawLine (transform.position, hitright.point, Color.cyan);
				currentwaypoints = waypoints;
			}
		}
		steering = newsteering;
		RightFront.steerAngle = steering;
		LeftFront.steerAngle = steering;
		//print(braketorque);

		RightFront.motorTorque = motortorque;
		LeftFront.motorTorque = motortorque;
		RightBack.motorTorque = motortorque;
		LeftBack.motorTorque = motortorque;
		RightFront.brakeTorque = braketorque;
		LeftFront.brakeTorque = braketorque;
		RightBack.brakeTorque = braketorque;
		LeftBack.brakeTorque = braketorque;
	}
	void brake(){
		float brakeforce = braketorque+=GetComponent<Rigidbody>().velocity.magnitude*80;
		motortorque = 0;
		braketorque = Mathf.Clamp(brakeforce, 60.0f, 1000.0f);
	}
	void drive(float distance){
		float torque = distance*0.2f;
		braketorque = 0;
		motortorque = Mathf.Clamp(motortorque+=torque, 0.0f, 300.0f);
		
	}
}
