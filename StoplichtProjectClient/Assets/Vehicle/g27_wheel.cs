using UnityEngine;
using System.Collections;

public class g27_wheel : MonoBehaviour {
	float previousWheelValue = 0f;

	// Update is called once per frame
	void FixedUpdate () {
		// Get the wheel value. Initial is 0.
		float wheelValue = Input.GetAxis ("SteeringWheel") * -1;

		// Set rotation.
		float newWheelValue = wheelValue - previousWheelValue;
		transform.RotateAround (transform.position, transform.forward, newWheelValue * 450);

		// Set previous wheel value.
		previousWheelValue = wheelValue;
	}
}
