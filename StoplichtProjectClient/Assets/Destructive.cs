using UnityEngine;
using System.Collections;

public class Destructive : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		Rigidbody newrig = GetComponent<Rigidbody>();
		newrig.isKinematic = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
