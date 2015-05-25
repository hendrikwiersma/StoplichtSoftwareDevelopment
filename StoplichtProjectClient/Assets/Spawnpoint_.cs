using UnityEngine;
using System.Collections;

public class Spawnpoint_ : MonoBehaviour {

	//public enum Direction{Noord, Oost, Zuid, West, Ventweg};
	//public enum Vehicle{Auto, Fiets, Bus, Voetganger};

	//public Direction direction;
	//public Vehicle vehicle;
	
	void Start () {
	
		ClientConnect port = GameObject.Find ("ClientConnect").GetComponent<ClientConnect> ();
		port.registerSpawnpoint(this.gameObject);

	}

	/*
	public void OnDrawGizmos()	{

		// bugged inialization outside code
		Vector3 gizmoSize = new Vector3 (1, 1, 1);
		
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, gizmoSize);
		
	}*/

}
