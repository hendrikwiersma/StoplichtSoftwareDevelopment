using UnityEngine;
using System.Collections;

public class WaitingScript : MonoBehaviour {
	public bool Go = false;
	// Use this for initialization
	public void SetGoBool(bool boolean){
		Go = boolean;
	}
	void OnTriggerExit(Collider other) {
		AIControllerWheelCol controller = other.gameObject.GetComponent<AIControllerWheelCol>();
		controller.go = true;
	}
}
