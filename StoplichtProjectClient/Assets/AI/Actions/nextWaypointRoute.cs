using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction("Next waypoint route")]
public class nextWaypointRoute : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai) {

		// Get AI data
		string direction = ai.WorkingMemory.GetItem<string> ("Direction");
		GameObject currentNetwork = ai.WorkingMemory.GetItem<GameObject> ("Network");

		// Init network when not set
		if (currentNetwork == null) {

			BikeAI mem = ai.Body.GetComponent<BikeAI>();
			currentNetwork = mem.Spawnpoint;
			direction = mem.Direction;
			ai.WorkingMemory.SetItem<string> ("Direction", direction);

		}

		// Obtain new route
		Intersection currentIntersection = currentNetwork.transform.parent.gameObject.GetComponent<Intersection>();
		GameObject nextRoute = currentIntersection.GetNewRoute (direction);

		// Check if at destination
		if (nextRoute == GameObject.Find ("Destenation")) {

			Debug.Log("Destenation reached");
			Object.Destroy(ai.Body.gameObject);
			return ActionResult.FAILURE;
			
		}

		// Obtain route target
		GameObject nextTarget = nextRoute.transform.Find("Target").gameObject;

		// Set data to AI
		ai.WorkingMemory.SetItem<Intersection> ("LatestIntersection", currentIntersection);
		ai.WorkingMemory.SetItem<GameObject> ("Network", nextRoute);
		ai.WorkingMemory.SetItem<GameObject> ("Destenation", nextTarget);
		ai.WorkingMemory.SetItem<bool> ("execute", true);

		// Give result for decisiontree
        return ActionResult.SUCCESS;

    }

}