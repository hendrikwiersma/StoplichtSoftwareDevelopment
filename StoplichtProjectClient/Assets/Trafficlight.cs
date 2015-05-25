using UnityEngine;
using System.Collections;

public abstract class Trafficlight : MonoBehaviour {

	public enum Direction{Noord,Oost,Zuid,West}
	public Direction direction;
	public int id = 0;
	public string State = "Rood";

	// Use this for initialization
	public void _Start () {
	
		ClientConnect port = GameObject.Find ("ClientConnect").GetComponent<ClientConnect> ();
		port.registerLight (this);

		SetState ();

	}

	public void setNewState(string newState) {

		if (newState != "Rood" && newState != "Oranje" && newState != "Groen") {
			
			Debug.Log ("Incorrect colour");
			return;
			
		}
		
		State = newState;
		SetState();
		
	}

	protected abstract void SetState();

}
