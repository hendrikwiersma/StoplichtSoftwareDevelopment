using UnityEngine;
using System.Collections;

public class FollowCar : MonoBehaviour {
	public Transform GameObject;

	// Update is called once per frame
	void Update () {
		transform.position = GameObject.position;
	}	
}
