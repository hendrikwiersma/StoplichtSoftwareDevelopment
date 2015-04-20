using UnityEngine;
using System.Collections;

public class ResetCar : MonoBehaviour {
	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.R) && timePassed >= keyDelay) {
			transform.rotation = Quaternion.identity;
			timePassed = 0f;
		}
	}
}
