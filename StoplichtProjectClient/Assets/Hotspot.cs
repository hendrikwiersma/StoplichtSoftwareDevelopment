using UnityEngine;
using System.Collections;

public class Hotspot : MonoBehaviour {
	public ClientConnect NetworkScript;
	public int TrafficLightID;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) {
		NetworkScript.SendVehicleSignal (TrafficLightID, 1);
	}
	void OnTriggerExit(Collider other) {
		NetworkScript.SendVehicleSignal (TrafficLightID, 0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
