using UnityEngine;
using System.Collections;

// Temporary data holder for route info
// Requested by AI when initialized
public class AIData : MonoBehaviour {

	public GameObject Spawnpoint;
	public string Direction;

	// Set new data at start
	public void Init(GameObject spawnpoint, Data.DIRECTION direction) {

		Spawnpoint = spawnpoint;
		switch (direction) {

			case Data.DIRECTION.NORTH: Direction = "North"; break;
			case Data.DIRECTION.EAST: Direction = "East"; break;
			case Data.DIRECTION.SOUTH: Direction = "South"; break;
			case Data.DIRECTION.WEST: Direction = "West"; break;
			case Data.DIRECTION.VENTWEG: Direction = "Ventweg"; break;
			default: Debug.Log("Illegal direction: " + direction); break;

		}

	}

}
