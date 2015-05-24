using UnityEngine;
using System.Collections;

public class parking_trigger : MonoBehaviour {
	public GameObject parkingSpace;

	void OnTriggerEnter(Collider other) {
		parking_space_controller controller = parkingSpace.GetComponent<parking_space_controller>();
		controller.addTrigger();
	}
	void OntriggerExit(Collider other){
		parking_space_controller controller = parkingSpace.GetComponent<parking_space_controller>();
		controller.removeTrigger();
	}
}
