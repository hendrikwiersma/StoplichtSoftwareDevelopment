using UnityEngine;
using System.Collections;

public class BikeAI : MonoBehaviour {

	public GameObject Spawnpoint;
	public string Direction;

	public void Init(GameObject spawnpoint, string direction) {

		Spawnpoint = spawnpoint;
		switch (direction) {

		case "Noord": Direction = "North"; break;
		case "Oost": Direction = "East"; break;
		case "Zuid": Direction = "South"; break;
		case "West": Direction = "West"; break;
		default: Debug.Log("Illegal direction: " + direction); break;

		}

	}

}
