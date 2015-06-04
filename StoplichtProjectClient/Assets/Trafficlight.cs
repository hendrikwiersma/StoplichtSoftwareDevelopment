using UnityEngine;
using System.Collections;

public abstract class Trafficlight : MonoBehaviour {

	public Data.DIRECTION Direction;
	public int id = 0;
	public Data.LIGHT_STATE State;
	
	public void _Start () {
	
		// Register to clientconnect as trafficlight
		ClientConnect port = GameObject.Find ("ClientConnect").GetComponent<ClientConnect> ();
		port.registerLight (this);

		// Update elements with new state
		SetState ();

	}

	// Set new trafficlight state
	public void setNewState(Data.LIGHT_STATE newState) {

		// Check for unknow state
		if (newState == Data.LIGHT_STATE.NULL) {
			
			Debug.Log ("Incorrect color");
			return;
			
		}

		// Update state
		State = newState;
		SetState();
		
	}
	
	protected abstract void SetState();

}
