using UnityEngine;
using System.Collections;

public class Intersection : MonoBehaviour {

	// follow different path if cant travel in certain direction
	private string USE_NORTH = "";
	private string USE_EAST;
	private string USE_SOUTH;
	private string USE_WEST;

	// Waypoint routes to get to intersection in this direction
	public GameObject[] North = new GameObject[1];
	public GameObject[] East = new GameObject[1];
	public GameObject[] South = new GameObject[1];
	public GameObject[] West = new GameObject[1];

	// traffic allowed to pass in this direction (n,e,s,w)
	public bool[] LightState = {true, true, true, true};

	// standing at this intersection with 'direction' in mind
	// wich route should I take
	public GameObject GetNewRoute(string direction) {

		GameObject[] dir;

		switch (direction) {

			case "North": dir = GetCorrectRoute(North); break;
			case "East": dir = GetCorrectRoute(East); break;
			case "South": dir = GetCorrectRoute(South); break;
			case "West": dir = GetCorrectRoute(West); break;
			default:
				Debug.LogError("Unable to find route for intersection");
				return null;

		}

		return dir [Random.Range (0, dir.Length)];

	}

	// Debug if direction exists
	// Switch route in case of "USE_DIRECTION"
	private GameObject[] GetCorrectRoute(GameObject[] routeChoice) {

		if (routeChoice == null || routeChoice [0] == null) {

			Debug.LogError("No route was set for intersection");
			return null;

		}

		initUSE_DIRECTION ();
		string route = routeChoice [0].ToString ();

		if (route == USE_NORTH) {
			return North;
		} else if (route == USE_EAST) {
			return East;
		} else if (route == USE_SOUTH) {
			return South;
		} else if (route == USE_WEST) {
			return West;
		} else {
			return routeChoice;
		}

	}

	// create USE_DIRECTION values
	private void initUSE_DIRECTION() {

		if (USE_NORTH == "") {

			USE_NORTH = GameObject.Find ("UseNorth").ToString ();
			USE_EAST = GameObject.Find ("UseEast").ToString ();
			USE_SOUTH = GameObject.Find ("UseSouth").ToString ();
			USE_WEST = GameObject.Find ("UseWest").ToString ();

		}

	}

	// Apply new lightstate, for incomming vehicles
	public void SetLightState(string Direction, string Newstate) {

		int direction = 0;

		switch (Direction) {

			case "Noord": direction = 0; break;
			case "Oost": direction = 1; break;
			case "Zuid": direction = 2; break;
			case "West": direction = 3; break;

		}
		LightState [0] = false;

		switch (Newstate) {

			case "Rood": LightState[direction] = false; break;
			case "Groen": LightState[direction] = true; break;

		}

	}

	// Get the lightstate for a certain direction
	public bool getLightState(string Direction) {

		switch (Direction) {
			
			case "Noord": return LightState[0];
			case "Oost": return LightState[1];
			case "Zuid": return LightState[2];
			case "West": return LightState[3];
			
		}

		return false;
		
	}

	// Get the lightstate in string format
	public string getLightState_(string Direction) {

		bool state = false;

		switch (Direction) {
			
			case "Noord": state = LightState[0]; break;
			case "Oost": state = LightState[1]; break;
			case "Zuid": state = LightState[2]; break;
			case "West": state = LightState[3]; break;
			
		}

		if (state) {

			return "Groen";

		} else {

			return "Rood";

		}

	}

}
