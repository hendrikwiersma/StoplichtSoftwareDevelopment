using UnityEngine;
using System.Collections;

public class parking_space_controller : MonoBehaviour {
	private int triggerCounter = 0;
	public int numberOfTriggers = 2;
	public void addTrigger(){
		triggerCounter++;
		if(triggerCounter >= numberOfTriggers){
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
	}
	public void removeTrigger(){
		triggerCounter--;
	}
}
