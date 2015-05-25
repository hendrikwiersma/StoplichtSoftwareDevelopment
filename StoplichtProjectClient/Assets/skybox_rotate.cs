using UnityEngine;
using System.Collections;

public class skybox_rotate : MonoBehaviour {
	public GameObject User;
	public float speed = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, speed*Time.deltaTime);
		transform.position = User.transform.position;
	}
}
