using UnityEngine;
using System.Collections;

public class TrafficlighBicycle : Trafficlight {

	// Use this for initialization
	void Start () {

		base._Start ();
	
	}

	protected override void SetState() {

		Intersection intersection = transform.parent.GetComponent<Intersection>();
		intersection.SetLightState(direction.ToString(), State.ToString());

	}

}
