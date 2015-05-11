using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {
	public float keyDelay = 1f;  // 1 second
	public Rigidbody rb;
	private float timePassed = 0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.U) && timePassed >= keyDelay) {		
			Vector3 velocity = rb.velocity;
			velocity.y = velocity.y + 30.0f;
			print (velocity);
			rb.velocity = velocity;
			timePassed = 0f;
		}
	}
}
