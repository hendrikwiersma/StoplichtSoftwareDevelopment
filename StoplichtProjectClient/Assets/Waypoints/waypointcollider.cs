using UnityEngine;
using System.Collections;

public class waypointcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {



		AIControllerWheelCol controller = other.gameObject.GetComponent<AIControllerWheelCol>();

		if (controller == null) {

			Debug.Log ("test");
			return;

		}

		Road roadScript = controller.WaypointCollection.GetComponent<Road>();
		Road parent = transform.parent.parent.GetComponent<Road>();

		if(controller.WaypointCollection.transform.name == transform.parent.parent.name && roadScript.number == parent.number){

			controller.nextWaypoint();

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
