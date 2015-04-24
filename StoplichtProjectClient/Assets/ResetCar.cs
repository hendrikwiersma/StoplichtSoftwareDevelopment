using UnityEngine;
using System.Collections;

public class ResetCar : MonoBehaviour {
	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	private Quaternion defaultrotation;
	private Vector3 defaulttranslation;
	// Use this for initialization
	void Start () {
		defaultrotation = transform.rotation;
		defaulttranslation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.R) && timePassed >= keyDelay) {
			transform.rotation = defaultrotation;
			transform.position = defaulttranslation;
			timePassed = 0f;
		}
	}
}
