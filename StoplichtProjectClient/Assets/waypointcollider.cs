using UnityEngine;
using System.Collections;

public class waypointcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {
		AIControllerWheelCol controller = other.gameObject.GetComponent<AIControllerWheelCol>();
		if(controller.WaypointCollection.transform.name == transform.parent.parent.name){
			controller.nextWaypoint();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
