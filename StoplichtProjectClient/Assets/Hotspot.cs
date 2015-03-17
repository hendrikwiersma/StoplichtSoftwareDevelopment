using UnityEngine;
using System.Collections;

public class Hotspot : MonoBehaviour {
	public ClientConnect NetworkScript;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {
		NetworkScript.SendVehicleSignal (0, 1);
		NetworkScript.SendVehicleSignal (55, 0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
