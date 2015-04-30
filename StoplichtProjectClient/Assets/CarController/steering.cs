using UnityEngine;
using System.Collections;

public class steering : MonoBehaviour {
	public WheelColliderSource fr;
	public WheelColliderSource fl;
	public WheelColliderSource br;
	public WheelColliderSource bl;

	public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		br.MotorTorque = 1000 * Input.GetAxis ("Vertical");
		bl.MotorTorque = 1000 * Input.GetAxis ("Vertical");
		fl.MotorTorque = 1000 * Input.GetAxis ("Vertical");
		fr.MotorTorque = 1000 * Input.GetAxis ("Vertical");

		//print(100.0f  * Input.GetAxis("Horizontal"));
		fl.SteerAngle = 100.0f  * Input.GetAxis("Horizontal");
		fr.SteerAngle = 100.0f  *  Input.GetAxis("Horizontal");
	}
}
