using UnityEngine;
using System.Collections;

public class speedometer : MonoBehaviour {
	public Rigidbody rb;
	private Vector3 initialrot;
	private float previousMagnitude = 0;

	// Use this for initialization
	void Start () {
		initialrot = transform.localRotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = rb.velocity;
		float magnitude = velocity.magnitude;

		// See how much we've accelerated (can be negative if our speed is less than the previous frame).
		float newMag = magnitude - previousMagnitude;

		// Set rotation.
		transform.RotateAround (transform.position, transform.up, newMag * (180 / 36));

		// Set previous magnitude so we can use it the next cycle.
		previousMagnitude = magnitude;
	}
}
