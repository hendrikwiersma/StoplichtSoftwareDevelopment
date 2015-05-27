using UnityEngine;
using System.Collections;

public class Speedometer_RPM : MonoBehaviour {
	private RCCCarControllerV2 carController;
	public Transform t;
	private const float baseOfMeter = 36f / 1325f;
	private float previousRPM;

	// Initialize some variables.
	void Start() {
		carController = t.GetComponent<RCCCarControllerV2> ();
	}

	// Update is called once per frame
	void Update () {
		float RPM = carController.EngineRPM;

		if (previousRPM != RPM) {
			float newRPM = RPM - previousRPM;

			// Set rotation.
			transform.RotateAround (transform.position, transform.forward, (newRPM * baseOfMeter));

			// Note down the RPM we've just used.
			previousRPM = RPM;
		}
	}
}
