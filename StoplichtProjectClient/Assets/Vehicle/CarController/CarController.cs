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

public class CarController : MonoBehaviour
{
	public bool useSteeringWheel;
	public Transform SteeringWheel;

	public Animator animator;

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

		FrontRightWheel.WheelRadius = FrontRight.GetComponent<Renderer> ().bounds.size.y / 2;
		FrontLeftWheel.WheelRadius = FrontLeft.GetComponent<Renderer> ().bounds.size.y / 2;
		BackRightWheel.WheelRadius = BackRight.GetComponent<Renderer> ().bounds.size.y / 2;
		BackLeftWheel.WheelRadius = BackLeft.GetComponent<Renderer> ().bounds.size.y / 2;

		Stabiliser front = gameObject.AddComponent<Stabiliser>();
		Stabiliser back = gameObject.AddComponent<Stabiliser> ();

		front.WheelL = FrontLeftWheel;
		front.WheelR = FrontRightWheel;

		back.WheelL = BackLeftWheel;
		back.WheelR = BackRightWheel;

    }

    public void FixedUpdate()
    {
		if (useSteeringWheel) {
			float value = Mathf.Lerp(0, 1, (Input.GetAxis ("SteerRight") + 1) * 0.5f);
			
			//print ("Steering Raw" + valueraw);
			//print ("Steering" + value);
			float AcceleratePedal = Mathf.Lerp(0, 1, (Input.GetAxisRaw ("AcceleratePedal") + 1) * 0.5f);
			print ("Steer" + value);
			
			//Apply the accelerator pedal
			FrontRightWheel.MotorTorque = AcceleratePedal * 3000.0f;
			//FrontLeftWheel.MotorTorque = Input.GetAxis ("Vertical") * -300.0f;

			//Turn the steering wheel
			FrontRightWheel.SteerAngle = Input.GetAxis ("SteerRight") * 45;
			FrontLeftWheel.SteerAngle = Input.GetAxis ("SteerRight") * 45;
			
			animator.SetFloat("SteerRight",value);			
			
			//Apply the hand braker
			if (Input.GetKey (KeyCode.Space)) {
				BackRightWheel.BrakeTorque = 200000.0f;
				BackLeftWheel.BrakeTorque = 200000.0f;
			} else { //Remove handbrake
				BackRightWheel.BrakeTorque = 0;
				BackLeftWheel.BrakeTorque = 0;
			}
		} else {
//			print ("Vertical" + Input.GetAxis ("Vertical"));
//			print ("Horizontal" + Input.GetAxis ("Horizontal"));
			//Apply the accelerator pedal
			FrontRightWheel.MotorTorque = Input.GetAxis ("Vertical") * 300.0f;
			FrontLeftWheel.MotorTorque = Input.GetAxis ("Vertical") * -300.0f;
			
			//Turn the steering wheel
			FrontRightWheel.SteerAngle = Input.GetAxis ("Horizontal") * 45;
			FrontLeftWheel.SteerAngle = Input.GetAxis ("Horizontal") * 45 + 180;
			
			//Apply the hand brake
			if (Input.GetKey(KeyCode.Space))
			{
				BackRightWheel.BrakeTorque = 200000.0f;
				BackLeftWheel.BrakeTorque = 200000.0f;
			}
			else //Remove handbrake
			{
				BackRightWheel.BrakeTorque = 0;
				BackLeftWheel.BrakeTorque = 0;
			}
		}
    }
}