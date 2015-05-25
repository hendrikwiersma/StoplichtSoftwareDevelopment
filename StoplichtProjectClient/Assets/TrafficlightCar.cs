using UnityEngine;
using System.Collections;

public class TrafficlightCar : Trafficlight {

	// Use this for initialization
	void Start () {

		base._Start ();
	
	}

	protected override void SetState() {
		
		trafficlightscript scr = gameObject.GetComponent<trafficlightscript> ();
		if (scr != null) {
			
			scr.switchlight(State);
			
		}
		
	}

}
