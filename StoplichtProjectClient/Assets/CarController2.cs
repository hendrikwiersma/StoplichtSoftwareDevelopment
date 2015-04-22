/******************************************
 *http://forum.unity3d.com/threads/wheel-collider-source-project.64494/
 * CarController
 *  
 * This class was created by:
 * 
 * Nic Tolentino.
 * rotatingcube@gmail.com
 * 
 * I take no liability for it's use and is provided as is.
 * 
 * The classes are based off the original code developed by Unity Technologies.
 * 
 * You are free to use, modify or distribute these classes however you wish, 
 * I only ask that you make mention of their use in your project credits.
*/
using UnityEngine;
using System.Collections;

public class CarController2 : MonoBehaviour
{
	public bool useSteeringWheel;
	public Transform FrontRight;
	public Transform FrontLeft;
	public Transform BackRight;
	public Transform BackLeft;
	
	private WheelColliderSource FrontRightWheel;
	private WheelColliderSource FrontLeftWheel;
	private WheelColliderSource BackRightWheel;
	private WheelColliderSource BackLeftWheel;
	
	public void Start() {
		
		FrontRightWheel = FrontRight.gameObject.AddComponent<WheelColliderSource>();
		FrontLeftWheel = FrontLeft.gameObject.AddComponent<WheelColliderSource>();
		BackRightWheel = BackRight.gameObject.AddComponent<WheelColliderSource>();
		BackLeftWheel = BackLeft.gameObject.AddComponent<WheelColliderSource>();
		
		FrontRightWheel.WheelRadius = FrontRight.localScale.y;
		FrontLeftWheel.WheelRadius = FrontLeft.localScale.y;
		BackRightWheel.WheelRadius = BackRight.localScale.y;
		BackLeftWheel.WheelRadius = BackLeft.localScale.y;
		
	}
	
	public void FixedUpdate()
	{
		if (useSteeringWheel) {

			float valueraw = Mathf.Lerp(0, 1, (Input.GetAxisRaw ("SteerLeft") + 1) * 0.5f);
			float value = Mathf.Lerp(0, 1, (Input.GetAxis ("SteerLeft") + 1) * 0.5f);

			//print ("Steering Raw" + valueraw);
			//print ("Steering" + value);
			float AcceleratePedal = Mathf.Lerp(0, 1, (Input.GetAxisRaw ("AcceleratePedal") + 1) * 0.5f);
			print ("AcceleratePedal" + AcceleratePedal);

			//Apply the accelerator pedal
			FrontRightWheel.MotorTorque = AcceleratePedal * 300.0f;
			//FrontLeftWheel.MotorTorque = Input.GetAxis ("Vertical") * -300.0f;
			
			//Turn the steering wheel
			FrontRightWheel.SteerAngle = Input.GetAxis ("SteerRight") * 45;
			FrontLeftWheel.SteerAngle = Input.GetAxis ("SteerRight") * 45;


			
			//Apply the hand brake
			if (Input.GetKey (KeyCode.Space)) {
				BackRightWheel.BrakeTorque = 200000.0f;
				BackLeftWheel.BrakeTorque = 200000.0f;
			} else { //Remove handbrake
				BackRightWheel.BrakeTorque = 0;
				BackLeftWheel.BrakeTorque = 0;
			}
		} else {
			print ("Vertical" + Input.GetAxis ("Vertical"));
			print ("Horizontal" + Input.GetAxis ("Horizontal"));
			//Apply the accelerator pedal
			FrontRightWheel.MotorTorque = Input.GetAxis ("Vertical") * 300.0f;
			FrontLeftWheel.MotorTorque = Input.GetAxis ("Vertical") * -300.0f;
		
			//Turn the steering wheel
			FrontRightWheel.SteerAngle = Input.GetAxis ("Horizontal") * 45;
			FrontLeftWheel.SteerAngle = Input.GetAxis ("Horizontal") * 45 + 180;
		
			//Apply the hand brake
			if (Input.GetKey (KeyCode.Space)) {
				BackRightWheel.BrakeTorque = 200000.0f;
				BackLeftWheel.BrakeTorque = 200000.0f;
			} else { //Remove handbrake
				BackRightWheel.BrakeTorque = 0;
				BackLeftWheel.BrakeTorque = 0;
			}
		}
	}
}
