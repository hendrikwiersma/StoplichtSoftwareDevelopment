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

		Intersection nextIntersection = currentNetwork.transform.parent.gameObject.GetComponent<Intersection>();
		GameObject nextRoute = nextIntersection.GetNewRoute (direction);

		if (nextRoute == GameObject.Find ("Destenation")) {

			Debug.Log("Destenation reached");
			return ActionResult.FAILURE;
			
		}

		GameObject nextTarget = nextRoute.transform.Find("Target").gameObject;

		ai.WorkingMemory.SetItem<GameObject> ("Network", nextRoute);
		ai.WorkingMemory.SetItem<GameObject> ("Destenation", nextTarget);
		ai.WorkingMemory.SetItem<bool> ("execute", true);

        return ActionResult.SUCCESS;

    }

}