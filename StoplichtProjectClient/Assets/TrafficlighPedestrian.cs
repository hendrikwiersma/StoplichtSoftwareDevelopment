using UnityEngine;
using System.Collections;

public class TrafficlighPedestrian : Trafficlight {
		
		void Start () {
			
			base._Start ();
			
		}
		
		// Update intersection with trafficlight state
		protected override void SetState() {
			
			Intersection intersection = transform.parent.GetComponent<Intersection>();
			intersection.SetLightState(direction.ToString(), State.ToString());
			
		}
		
	}
