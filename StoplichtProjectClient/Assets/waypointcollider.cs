using UnityEngine;
using System.Collections;

public class waypointcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {
		//NetworkScript.SendVehicleSignal (TrafficLightID, 1);
		print("Entered waypoint collider.");
		AIControllerWheelCol controller = other.gameObject.GetComponent<AIControllerWheelCol>();
		controller.nextWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
