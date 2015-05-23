using UnityEngine;
using System.Collections;

public class SlowRotate : MonoBehaviour {
	public GameObject User;
	public float speed = 0.05f;
	public bool useUser = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, speed*Time.deltaTime);
		if(useUser){
			transform.position = User.transform.position;
		}
	}
}
