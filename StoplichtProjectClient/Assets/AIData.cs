using UnityEngine;
using System.Collections;

// Temporary data holder for route info
// Requested by AI when initialized
public class AIData : MonoBehaviour {

	public GameObject Spawnpoint;
	public string Direction;

	// Set new data at start
	public void Init(GameObject spawnpoint, string direction) {

		Spawnpoint = spawnpoint;
		switch (direction) {

			case "Noord": Direction = "North"; break;
			case "Oost": Direction = "East"; break;
			case "Zuid": Direction = "South"; break;
			case "West": Direction = "West"; break;
			case "Ventweg": Direction = "Ventweg"; break;
			default: Debug.Log("Illegal direction: " + direction); break;

		}

	}

}
