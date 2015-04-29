using UnityEngine;
using System.Collections;

public class steering : MonoBehaviour {
	public WheelCollider fr;
	public WheelCollider fl;
	public WheelCollider br;
	public WheelCollider bl;

	public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		br.motorTorque = 10000 * Input.GetAxis ("Vertical");
		bl.motorTorque = 10000 * Input.GetAxis ("Vertical");
		fl.motorTorque = 10000 * Input.GetAxis ("Vertical");
		fr.motorTorque = 10000 * Input.GetAxis ("Vertical");

		//print(100.0f  * Input.GetAxis("Horizontal"));
		fl.steerAngle = 100.0f  * Input.GetAxis("Horizontal");
		fr.steerAngle = 100.0f  *  Input.GetAxis("Horizontal");
	}
}
