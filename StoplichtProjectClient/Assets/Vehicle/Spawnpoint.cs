using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
	public enum Begin{Noord,Oost,Zuid,West,Ventweg}
	public enum End{Noord,Oost,Zuid,West,Ventweg,SelfSearching}
	public enum Vehicle{Auto,Fiets,Bus,Voetganger}
	public Begin begin;
	public End end;
	public Vehicle vehicle;
	public int StartWaypointCollection;
	public bool available = true;
	public int carsInside = 0;

	void OnTriggerEnter(Collider other) {
		carsInside++;
	}

	void OnTriggerExit(Collider other) {
		carsInside--;
	}

	public bool spawnpointAvailable(){
		if(carsInside == 0){
			return true;
		}
		else{
			return false;
		}
	}

	public void addCarInsideSpawnpoint(){
		carsInside++;
	}

	public void OnDrawGizmos()	{

		// bugged inialization outside code
		Vector3 gizmoSize = new Vector3 (1, 1, 1);

		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, gizmoSize);

	}

}
