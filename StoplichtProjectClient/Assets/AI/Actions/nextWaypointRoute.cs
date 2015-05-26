using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction("Next waypoint route")]
public class nextWaypointRoute : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai) {

		string direction = ai.WorkingMemory.GetItem<string> ("Direction");
		GameObject currentNetwork = ai.WorkingMemory.GetItem<GameObject> ("Network");

		if (currentNetwork == null) {

			BikeAI mem = ai.Body.GetComponent<BikeAI>();
			currentNetwork = mem.Spawnpoint;
			direction = mem.Direction;
			ai.WorkingMemory.SetItem<string> ("Direction", direction);

		}

		Intersection currentIntersection = currentNetwork.transform.parent.gameObject.GetComponent<Intersection>();
		GameObject nextRoute = currentIntersection.GetNewRoute (direction);

		if (nextRoute == GameObject.Find ("Destenation")) {

			Debug.Log("Destenation reached");
			Object.Destroy(ai.Body.gameObject);
			return ActionResult.FAILURE;
			
		}

		GameObject nextTarget = nextRoute.transform.Find("Target").gameObject;

		ai.WorkingMemory.SetItem<Intersection> ("LatestIntersection", currentIntersection);
		ai.WorkingMemory.SetItem<GameObject> ("Network", nextRoute);
		ai.WorkingMemory.SetItem<GameObject> ("Destenation", nextTarget);
		ai.WorkingMemory.SetItem<bool> ("execute", true);

        return ActionResult.SUCCESS;

    }

}