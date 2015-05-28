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
	public GameObject[] Ventweg = new GameObject[1];
	
	// traffic allowed to pass in this direction (n,e,s,w)
	public bool[] LightState = {true, true, true, true};

	// Amount of bikers currently in this lane
	public int[] bikersInLane = {0, 0, 0, 0};

	// Limit of bikers in this lane (0) = unlimited
	public int[] bikersMaxLane = {0, 0, 0, 0};

	// standing at this intersection with 'direction' in mind
	// wich route should I take
	public GameObject GetNewRoute(string direction) {

		GameObject[] dir;

		switch (direction) {

			case "North": dir = GetCorrectRoute(North); break;
			case "East": dir = GetCorrectRoute(East); break;
			case "South": dir = GetCorrectRoute(South); break;
			case "West": dir = GetCorrectRoute(West); break;
			case "Ventweg": dir = GetCorrectRoute(Ventweg); break;
			default:
				Debug.LogError("Unable to find route for intersection");
				return null;

		}

		return dir [Random.Range (0, dir.Length)];

	}

	// Debug if direction exists
	// Switch route in case of "USE_DIRECTION"
	private GameObject[] GetCorrectRoute(GameObject[] routeChoice) {

		if (routeChoice == null) {

			Debug.LogError("No route was set for intersection");
			return null;

		}

		if (routeChoice [0] == null) {

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

	public string findUsedDirection(string direction) {

		string dir = "";

		switch (direction) {
			case "North":
			dir = findUsedDirection_ (North, direction);
				break;
			case "East":
			dir = findUsedDirection_ (East, direction);
				break;
			case "South":
			dir = findUsedDirection_ (South, direction);
				break;
			case "West":
			dir = findUsedDirection_ (West, direction);
				break;
			case "Ventweg":
			dir = findUsedDirection_ (Ventweg, direction);
				break;
			default:
				Debug.LogError ("Unable to find route for intersection");
				break;
		}

		return dir;

	}

	private string findUsedDirection_(GameObject[] routeChoice, string direction) {

		initUSE_DIRECTION ();
		string route = routeChoice [0].ToString ();
		
		if (route == USE_NORTH) {
			return "North";
		} else if (route == USE_EAST) {
			return "East";
		} else if (route == USE_SOUTH) {
			return "South";
		} else if (route == USE_WEST) {
			return "West";
		} else {
			return direction;
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
			
			case "North": return LightState[0];
			case "East": return LightState[1];
			case "South": return LightState[2];
			case "West": return LightState[3];
			
		}

		Debug.LogError ("Unknown light state");
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
			default: Debug.LogError ("Unknown light state"); break;
			
		}

		if (state) {

			return "Groen";

		} else {

			return "Rood";

		}

	}

	public int getBikersMaxLane(string Direction) {

		switch (Direction) {
			
		case "North": return bikersMaxLane[0];
		case "East": return bikersMaxLane[1];
		case "South": return bikersMaxLane[2];
		case "West": return bikersMaxLane[3];
		default: Debug.LogError ("Unknown direction"); return 0;
			
		}
		
	}

	public int getBikersInLane(string Direction) {
		
		switch (Direction) {
			
		case "North": return bikersInLane[0];
		case "East": return bikersInLane[1];
		case "South": return bikersInLane[2];
		case "West": return bikersInLane[3];
		default: Debug.LogError ("Unknown direction"); return 0;
			
		}
		
	}
	
}
