using UnityEngine;
using System.Collections;

public class TrafficlighBicycle : Trafficlight {
	
	void Start () {

		base._Start ();
	
	}

	// Update intersection with trafficlight state
	protected override void SetState() {

		Intersection intersection = transform.parent.GetComponent<Intersection>();
		intersection.SetLightState(Direction, State);

	}

}
