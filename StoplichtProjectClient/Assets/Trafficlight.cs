using UnityEngine;
using System.Collections;

public abstract class Trafficlight : MonoBehaviour {

	public enum Direction{Noord,Oost,Zuid,West}
	public Direction direction;
	public int id = 0;
	public string State = "Rood";
	
	public void _Start () {
	
		// Register to clientconnect as trafficlight
		ClientConnect port = GameObject.Find ("ClientConnect").GetComponent<ClientConnect> ();
		port.registerLight (this);

		// Update elements with new state
		SetState ();

	}

	// Set new trafficlight state
	public void setNewState(string newState) {

		// Check for unknow state
		if (newState != "Rood" && newState != "Oranje" && newState != "Groen") {
			
			Debug.Log ("Incorrect colour");
			return;
			
		}

		// Update state
		State = newState;
		SetState();
		
	}
	
	protected abstract void SetState();

}
