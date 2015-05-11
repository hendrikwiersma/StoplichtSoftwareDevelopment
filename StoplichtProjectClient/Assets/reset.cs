using UnityEngine;
using System.Collections;

public class reset : MonoBehaviour {
	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	private Vector3 initialpos;
	private Quaternion initialrot;
	// Use this for initialization
	void Start () {
		initialpos = transform.position;
		initialrot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.R) && timePassed >= keyDelay) {		
			transform.position = initialpos;
			transform.rotation = initialrot;
			timePassed = 0f;
		}
	}
}
