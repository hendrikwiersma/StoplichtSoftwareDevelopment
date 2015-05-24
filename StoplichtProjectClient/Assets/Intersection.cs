using UnityEngine;
using System.Collections;

public class Intersection : MonoBehaviour {

	public GameObject[] North = null;
	public GameObject[] East = null;
	public GameObject[] South = null;
	public GameObject[] West = null;

	public GameObject GetNewRoute(string direction) {

		GameObject[] dir;

		switch (direction) {

			case "North":
				dir = North;
				break;

			case "East":
				dir = East;
				break;

			case "South":
				dir = South;
				break;

			case "West":
				dir = West;
				break;

			default:
				Debug.LogError("Unable to find route for intersection");
				return null;

		}

		if (dir == null || dir.Length == 0) {

			Debug.LogError("Unable to find route for intersection");
			return null;

		}

		return dir [Random.Range (0, dir.Length)];

	}

}
