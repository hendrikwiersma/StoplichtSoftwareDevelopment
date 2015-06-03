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

	
	public GameObject WaypointCollection1;
	public GameObject WaypointCollection2;

	public GameObject CurrentWaypoints;
	private int waypointcounter = 0;
	public Transform Target;

	public float timeBetweenSteps = 2.0f;
	private float lastStep;
	private float steering = 0.0f;
	private float braketorque = 0.0f;
	private float motortorque = 0.0f;
	private float distance = 0.0f;
	public bool go = true;
	public bool drawDebuglines = true;
	public GameObject RightOriginPoint;
	public GameObject LeftOriginPoint;
	public int topMotorTorque = 400;
	public int topBrakeTorque = 1000;

	private Rigidbody carRigidbody;
	private Road carRoad;

	// Use this for initialization
	void Start () {

		Target = CurrentWaypoints.transform.GetChild(waypointcounter);
		carRigidbody = GetComponent<Rigidbody>();

	}

	public void nextWaypoint(){
		if(waypointcounter < CurrentWaypoints.transform.childCount-1){
			waypointcounter++;
		}
		else{
			Destroy(this.gameObject);
		}
		Target = CurrentWaypoints.transform.GetChild(waypointcounter);
	}

	void Update () {
		float xDistance = Target.position.x - transform.position.x;
		float zDistance = Target.position.z - transform.position.z;
		float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
		float carAngle = transform.localEulerAngles.y;
		float steering = (angle - carAngle);
		distance = Vector3.Distance (Target.position, transform.position);

		RightFront.steerAngle = steering;
		LeftFront.steerAngle = steering;
		if(Time.time - lastStep > timeBetweenSteps){
		    lastStep = Time.time;

			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 left = transform.TransformDirection(Vector3.left);
			Vector3 right = transform.TransformDirection(Vector3.right);
			Vector3 frl = transform.TransformDirection(new Vector3(-0.5f, 0, 1));
			Vector3 frr = transform.TransformDirection(new Vector3(0.5f, 0, 1));
			
			RaycastHit[] forward_hits;
			RaycastHit[] left_forward_hits;
			RaycastHit[] right_forward_hits;
			RaycastHit[] left_sideways_hits;
			RaycastHit[] right_sideways_hits;
			RaycastHit[] stoplights_check_hits;

			RaycastHit[] left_hits = null;
			RaycastHit[] right_hits = null;

			float Magnitude = carRigidbody.velocity.magnitude;

			left_forward_hits = Physics.RaycastAll(LeftOriginPoint.transform.position, forward, Mathf.Clamp(Magnitude, 10, 400));
			right_forward_hits = Physics.RaycastAll(RightOriginPoint.transform.position, forward, Mathf.Clamp(Magnitude, 10, 400));
			left_sideways_hits = Physics.RaycastAll(LeftOriginPoint.transform.position, frl, Mathf.Clamp(Magnitude, 10, 50));
			right_sideways_hits = Physics.RaycastAll(RightOriginPoint.transform.position, frr, Mathf.Clamp(Magnitude, 10, 50));
			stoplights_check_hits = Physics.RaycastAll(transform.position, forward, Mathf.Clamp(Magnitude, 20, 500));
			
			forward_hits = left_forward_hits.Concat(right_forward_hits).ToArray();
			forward_hits = forward_hits.Concat(left_sideways_hits).ToArray();
			forward_hits = forward_hits.Concat(right_sideways_hits).ToArray();

			bool car_in_front = false;
			bool car_to_left = false;
			bool car_to_right = false;
			
			for (int i = 0; i < stoplights_check_hits.Length; i++) {
				RaycastHit hit = stoplights_check_hits[i];
				if(hit.collider.gameObject.tag == "Trafficlight"){
				WaitingScript controller = hit.collider.gameObject.GetComponent<WaitingScript>();
					if(controller.waypointSystem.ToString() == CurrentWaypoints.transform.name){
						if(controller.Go == true){
							go = true;
						}
						else{
							go = false;
						}
					}
				}
			}
			
			for (int i = 0; i < forward_hits.Length; i++) {
				RaycastHit hit = forward_hits[i];
				if(hit.collider.gameObject.tag == "Car"){
					car_in_front = true;
					//print(hit.collider);
				}
			}

			Road roadScript = CurrentWaypoints.GetComponent<Road>();
			if(go == true){
				if(roadScript.number == 0 && car_in_front == true){
					left_hits = Physics.RaycastAll(LeftOriginPoint.transform.position, left, 5.0f);
					for (int i = 0; i < left_hits.Length; i++) {
						RaycastHit hit = left_hits[i];
						if(hit.collider.gameObject.tag == "Car"){
							car_to_left = true;
						}
					}
					if(car_to_left == false && waypointcounter % 5 == 0){
						CurrentWaypoints = WaypointCollection2;
						brake();
					}
					else{
						brake();
					}
				}
				else if(roadScript.number == 0 && car_in_front == false){
					drive();
				}
				else if(roadScript.number == 1 && car_in_front == false){
					right_hits = Physics.RaycastAll(RightOriginPoint.transform.position, right, 5.0f);
					for (int i = 0; i < right_hits.Length; i++) {
						RaycastHit hit = right_hits[i];
						if(hit.collider.gameObject.tag == "Car"){
							car_to_right = true;
						}
					}
					if(car_to_right == false && waypointcounter % 5 == 0){
						CurrentWaypoints = WaypointCollection1;
						brake();
					}
					else{
						drive();
					}
				}
				else if(roadScript.number == 1 && car_in_front == true){
					brake();
				}
			}
			else{
				brake();
			}


			if(drawDebuglines){
				Debug.DrawLine (transform.position, Target.transform.position, Color.white);
				for (int i = 0; i < right_forward_hits.Length; i++) {
					RaycastHit hit = right_forward_hits[i];
					Debug.DrawLine (RightOriginPoint.transform.position, hit.point, Color.cyan);
				}
				for (int i = 0; i < left_forward_hits.Length; i++) {
					RaycastHit hit = left_forward_hits[i];
					Debug.DrawLine (LeftOriginPoint.transform.position, hit.point, Color.cyan);
				}
				for (int i = 0; i < left_sideways_hits.Length; i++) {
					RaycastHit hit = left_sideways_hits[i];
					Debug.DrawLine (LeftOriginPoint.transform.position, hit.point, Color.cyan);
				}
				for (int i = 0; i < left_sideways_hits.Length; i++) {
					RaycastHit hit = left_sideways_hits[i];
					Debug.DrawLine (RightOriginPoint.transform.position, hit.point, Color.cyan);
				}
				if(left_hits != null){
					for (int i = 0; i < left_hits.Length; i++) {
						RaycastHit hit = left_hits[i];
						Debug.DrawLine (LeftOriginPoint.transform.position, hit.point, Color.red);
					}
				}
				if(right_hits != null){
					for (int i = 0; i < right_hits.Length; i++) {
						RaycastHit hit = right_hits[i];
						Debug.DrawLine (RightOriginPoint.transform.position, hit.point, Color.red);
					}
				}
			}
		}
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
		
		float brakeforce = braketorque+=carRigidbody.velocity.magnitude*300;
		motortorque = 0;
		braketorque = Mathf.Clamp(brakeforce, 20, topBrakeTorque);
		//print("Brake" + braketorque);

	}
	void drive(){
		
		float torque = distance*8.0f;
		braketorque = 0;
		motortorque = Mathf.Clamp(motortorque+=torque, 0.0f, topMotorTorque);
		//print("Drive");
	}
}
