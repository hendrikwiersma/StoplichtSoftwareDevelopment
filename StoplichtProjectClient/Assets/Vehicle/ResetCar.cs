using UnityEngine;
using System.Collections;

public class ResetCar : MonoBehaviour {

	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	private Quaternion defaultrotation;
	private Vector3 defaulttranslation;
	private Rigidbody r;
	// Use this for initialization
	void Start () {

		defaultrotation = transform.rotation;
		defaulttranslation = transform.position;

		r = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		timePassed += Time.deltaTime;

		if (Input.GetKey(KeyCode.R) && timePassed >= keyDelay) {

			transform.rotation = defaultrotation;
			transform.position = defaulttranslation;
			timePassed = 0f;

			r.velocity = Vector3.zero;
			r.angularVelocity = Vector3.zero;

		}

	}

}