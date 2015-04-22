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

	public WheelCollider FrontRightWheelCol;
	public WheelCollider FrontLeftWheelCol;
	public WheelCollider BackRightWheelCol;
	public WheelCollider BackLeftWheelCol;

    public void Start() {

		FrontRightWheelCol.radius = FrontRight.localScale.y;
		FrontLeftWheelCol.radius = FrontLeft.localScale.y;
		BackRightWheelCol.radius = BackRight.localScale.y;
		BackLeftWheelCol.radius = BackLeft.localScale.y;

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
			FrontRightWheelCol.motorTorque = AcceleratePedal * 3000.0f;
			//FrontLeftWheel.MotorTorque = Input.GetAxis ("Vertical") * -300.0f;
			
			//Turn the steering wheel
			FrontRightWheelCol.steerAngle = Input.GetAxis ("SteerRight") * 45;
			FrontLeftWheelCol.steerAngle = Input.GetAxis ("SteerRight") * 45;
			
			animator.SetFloat("SteerRight",value);			
			
			//Apply the hand brake
			if (Input.GetKey (KeyCode.Space)) {
				BackRightWheelCol.brakeTorque = 200000.0f;
				BackLeftWheelCol.brakeTorque = 200000.0f;
			} else { //Remove handbrake
				BackRightWheelCol.brakeTorque = 0;
				BackLeftWheelCol.brakeTorque = 0;
			}
		} else {
			print ("Vertical" + Input.GetAxis ("Vertical"));
			print ("Horizontal" + Input.GetAxis ("Horizontal"));
			//Apply the accelerator pedal
			FrontRightWheelCol.motorTorque = Input.GetAxis ("Vertical") * 300.0f;
			FrontLeftWheelCol.motorTorque = Input.GetAxis ("Vertical") * -300.0f;
			
			//Turn the steering wheel
			FrontRightWheelCol.steerAngle = Input.GetAxis ("Horizontal") * 45;
			FrontLeftWheelCol.steerAngle = Input.GetAxis ("Horizontal") * 45 + 180;
			
			//Apply the hand brake
			if (Input.GetKey (KeyCode.Space)) {
				BackRightWheelCol.brakeTorque = 200000.0f;
				BackLeftWheelCol.brakeTorque = 200000.0f;
			} else { //Remove handbrake
				BackRightWheelCol.brakeTorque = 0;
				BackLeftWheelCol.brakeTorque = 0;
			}
		}
    }
}
