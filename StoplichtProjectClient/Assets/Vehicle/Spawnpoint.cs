using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
	public enum Direction{Noord,Oost,Zuid,West,Ventweg}
	public enum Vehicle{Auto,Fiets,Bus,Voetganger}
	public Direction direction;
	public Vehicle vehicle;
	public int StartWaypointCollection;
	public bool available = true;
	// Use this for initialization
	void OnTriggerExit(Collider other) {
		available = true;
	}
	void Start () {
	
	}
	public void OnDrawGizmos()	{

		// bugged inialization outside code
		Vector3 gizmoSize = new Vector3 (1, 1, 1);

		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, gizmoSize);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
