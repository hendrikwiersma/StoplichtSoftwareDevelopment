using UnityEngine;
using System.Collections;

public class waypointcollider : MonoBehaviour {
	private Road roadObject;
	private string waypointName;
	private string vehicleType;

	// Use this for initialization
	void Start () {
		roadObject = transform.parent.parent.GetComponent<Road>();
		waypointName = transform.parent.parent.name;
		vehicleType = transform.parent.parent.parent.name;
	}
	void OnTriggerEnter(Collider other) {

		AICarController carController = other.gameObject.GetComponent<AICarController>();
		if (carController != null) {
			Road roadScript = carController.CurrentWaypoints.GetComponent<Road>();
			if(carController.CurrentWaypoints.transform.name == waypointName && roadScript.number == roadObject.number && vehicleType == carController.vehicleType){
				carController.nextWaypoint();
			}
		}
		AIBusController busController = other.gameObject.GetComponent<AIBusController>();
		if (busController != null) {
			Road roadScript = busController.CurrentWaypoints.GetComponent<Road>();
			if(busController.CurrentWaypoints.transform.name == waypointName && roadScript.number == roadObject.number && vehicleType == busController.vehicleType){
				busController.nextWaypoint();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
