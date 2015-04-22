	using UnityEngine;
using System.Collections;

public class newwheelmeshscript : MonoBehaviour {
	public WheelCollider wheelcollider;
	public Transform carobject;
	public float maxoutpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float rpm = wheelcollider.rpm;	
		transform.Rotate (new Vector3 (rpm/10, 0.0f, 0.0f));
		RaycastHit hit;
		Ray newray = new Ray(wheelcollider.transform.position, -carobject.transform.up);
		Debug.DrawRay (wheelcollider.transform.position, -carobject.transform.up, Color.green, 2, true);
		if(Physics.Raycast(newray, out hit, maxoutpos)){
			transform.position = new Vector3(hit.point.x, hit.point.y + maxoutpos/2, hit.point.z);
		}
	}
}