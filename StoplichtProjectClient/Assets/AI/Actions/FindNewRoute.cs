using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FindNewRoute : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai) {

		// Get AI data
		string direction = ai.WorkingMemory.GetItem<string> ("Direction");
		Intersection currentIntersection = ai.WorkingMemory.GetItem<Intersection> ("Intersection");
		
		// Init network when not set
		if (currentIntersection == null) {
			
			AIData mem = ai.Body.GetComponent<AIData>();
			GameObject spawnpoint = mem.Spawnpoint;
			currentIntersection = spawnpoint.transform.parent.gameObject.GetComponent<Intersection>();
			direction = mem.Direction;
			ai.WorkingMemory.SetItem<string> ("Direction", direction);
			
		}

		// Obtain new route
		GameObject nextNetwork = currentIntersection.GetNewRoute (direction);
		
		// Check if at destination
		if (nextNetwork == GameObject.Find ("Destenation")) {
			
			Debug.Log("Destenation reached");
			Object.Destroy(ai.Body.gameObject);
			return ActionResult.FAILURE;
			
		}

		// == Debug find not assigned network
		if (nextNetwork == null) {

			Debug.LogError("Network not found for: " + currentIntersection);

		}
		
		// Obtain route target
		Intersection nextIntersection = nextNetwork.transform.parent.gameObject.GetComponent<Intersection>();
		GameObject nextTarget = nextNetwork.transform.Find("Target").gameObject;

		// Set data to AI
		ai.WorkingMemory.SetItem<Intersection> ("PrevIntersection", currentIntersection);
		ai.WorkingMemory.SetItem<Intersection> ("Intersection", nextIntersection);
		ai.WorkingMemory.SetItem<GameObject> ("Network", nextNetwork);
		ai.WorkingMemory.SetItem<GameObject> ("Destenation", nextTarget);
		ai.WorkingMemory.SetItem<bool> ("execute", true);

		return ActionResult.SUCCESS;

    }

}