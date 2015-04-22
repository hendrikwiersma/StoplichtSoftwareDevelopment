using UnityEngine;
using System.Collections;

public class ViewChanger : MonoBehaviour {
	ArrayList Viewlist = new ArrayList();
	int counter;
	public float keyDelay = 1f;  // 1 second
	private float timePassed = 0f;
	// Use this for initialization
	void Start () {
		counter = 0;
		Viewlist.Add (new Vector3 (-0.5f,1.2f,-0.2f));
		Viewlist.Add (new Vector3 (0.0f,0.0f,0.0f));
		Viewlist.Add (new Vector3 (0.0f,2.0f,0.0f));
		Viewlist.Add (new Vector3 (0.0f,2.0f,-5.0f));
		//print (transform.localPosition);

	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (Input.GetKey(KeyCode.C) && timePassed >= keyDelay) {
			counter ++;
			if(counter >= Viewlist.Count){
				counter = 0;
			}
			transform.localPosition = (Vector3)Viewlist[counter];
			timePassed = 0f;
		}
	}
}
