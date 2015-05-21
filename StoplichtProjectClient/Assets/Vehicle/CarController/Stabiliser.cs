using UnityEngine;
using System.Collections;

public class Stabiliser : MonoBehaviour {

	public WheelColliderSource WheelL;
	public WheelColliderSource WheelR;
	float AntiRoll = 1000.0f;
	
	public void FixedUpdate ()
	{
		WheelHitSource hit;
		float travelL = 1.0f;
		float travelR = 1.0f;

		bool groundedL = WheelL.GetGroundHit (out hit);
		if (groundedL)
			travelL = (-WheelL.transform.InverseTransformPoint(hit.Point).y - WheelL.WheelRadius) / WheelL.SuspensionDistance;
		
		bool groundedR = WheelR.GetGroundHit (out hit);
		if (groundedR)
			travelR = (-WheelR.transform.InverseTransformPoint(hit.Point).y - WheelR.WheelRadius) / WheelR.SuspensionDistance;
		
		var antiRollForce = (travelL - travelR) * AntiRoll;
		
		if (groundedL)
			GetComponent<Rigidbody>().AddForceAtPosition(WheelL.transform.up * -antiRollForce,
			                                              WheelL.transform.position); 
		if (groundedR)
			GetComponent<Rigidbody>().AddForceAtPosition(WheelR.transform.up * antiRollForce,
			                                              WheelR.transform.position);

	}

}