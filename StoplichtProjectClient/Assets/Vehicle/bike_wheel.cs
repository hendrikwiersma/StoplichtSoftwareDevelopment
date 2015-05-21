using UnityEngine;
using System.Collections;

public class bike_wheel : MonoBehaviour {
	public Rigidbody bikerigidbody;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float speed = bikerigidbody.velocity.magnitude;
		transform.RotateAround (transform.position, transform.right, speed);
	}
}
