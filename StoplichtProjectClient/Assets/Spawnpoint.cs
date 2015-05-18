using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
	public enum Direction{Noord,Oost,Zuid,West,Ventweg}
	public enum Vehicle{Auto,Fiets,Bus,Voetganger}
	public Direction direction;
	public Vehicle vehicle;
	public bool available = true;
	// Use this for initialization
	void OnTriggerExit(Collider other) {
		available = true;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
