using UnityEngine;
using System.Collections;

public class setWheel : MonoBehaviour {
	
	public Transform wheel;
	private WheelCollider wc;

	private bool isGrounded = false;
	private float compression; //0 is fully extended, 1 is fully compressed
	//private float force;

	// Use this for initialization
	void Start () {

		wc = GetComponent<WheelCollider> ();
		wc.center = wheel.localPosition;
		wc.radius = wheel.localScale.y;

	}
	
	// Update is called once per frame
	void Update () {

		setWheelPosition ();

	}

	private void setWheelPosition() {

		float travelDist = 0f;
		WheelHit wh; //this is the GroundHit

		if (wc.GetGroundHit(out wh)) {
			isGrounded = true;
		} else { isGrounded = false; }
		
		if(isGrounded) {
			//this is the current suspension travel in meters:
			travelDist = -wc.transform.InverseTransformPoint(wh.point).y - wc.radius;
			compression = (1-(travelDist / wc.suspensionDistance));
//			force = wh.force;
		} else {
			compression = 0;
//			force = 0;
		}
		
		Vector3 loc = wc.center;

		float c = 1 - compression;
		if (c != 0) {
		
			if (!isGrounded)
				loc.y = loc.y - wc.suspensionDistance * c;

			else 
				loc.y = - wc.suspensionDistance * c;
			//Debug.Log("suspens: " + wc.suspensionDistance);
			//Debug.Log("dist: " +loc.y);
			//Debug.Log("c: " + c);
		}
		
		wheel.localPosition = loc;

	}

}
