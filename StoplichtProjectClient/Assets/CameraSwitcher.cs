using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	private ArrayList cameralist = new ArrayList();
	private int counter = 0;
	// Use this for initialization
	void Start () {

		cameralist.Add(new Vector3(-0.5f,1.2f,-0.2f));
		cameralist.Add(new Vector3(0.0f,0.0f,0.0f));
		cameralist.Add(new Vector3(0.0f,3.0f,-5.0f));
		cameralist.Add(new Vector3(0.0f,-3.0f,0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.C) && timePassed >= keyDelay) {
			counter++;
			if(counter >= cameralist.Count){
				counter = 0;
			}


			transform.localPosition = (Vector3)cameralist[counter];
			timePassed = 0f;
		}
	}
}
