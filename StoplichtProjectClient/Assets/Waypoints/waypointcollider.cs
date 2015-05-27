using UnityEngine;
using System.Collections;

public class waypointcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {



		AIControllerWheelCol controller = other.gameObject.GetComponent<AIControllerWheelCol>();

		if (controller == null) {

			return;

		}

		Road roadScript = controller.CurrentWaypoints.GetComponent<Road>();
		Road parent = transform.parent.parent.GetComponent<Road>();

		if(controller.CurrentWaypoints.transform.name == transform.parent.parent.name){

			controller.nextWaypoint();

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
