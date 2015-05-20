using UnityEngine;
using System.Collections;

public class SlowRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, 0.5f*Time.deltaTime);
	}
}
