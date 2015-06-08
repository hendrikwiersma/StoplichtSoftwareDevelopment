using UnityEngine;
using System.Collections;

public class WaitingScript : MonoBehaviour {
	public bool Go = false;
	public enum WaypointSystem{Westoost,WestNoord,ZuidWest,ZuidNoord,WestZuid,ZuidOost,OostWest,OostNoord,OostZuid,NoordOost,NoordZuid,NoordWest}
	public WaypointSystem waypointSystem;
	public int TrafficLightID = 0;
	// Use this for initialization
	public void SetGoBool(bool boolean){
		Go = boolean;
	}
	void OnTriggerEnter(Collider other) {
		AICarController controller = other.gameObject.GetComponent<AICarController>();

		if (controller != null) {

			controller.go = true;

		}

	}
	void OnTriggerExit(Collider other) {
		AICarController controller = other.gameObject.GetComponent<AICarController>();

		if (controller != null) {

			controller.go = true;

		}

	}
	public void OnDrawGizmos()	{

		// bugged inialization outside code
		Vector3 gizmoSize = new Vector3 (3, 3, 3);

		Gizmos.color = Color.blue;
		Gizmos.DrawCube(transform.position, gizmoSize);
	}
}
