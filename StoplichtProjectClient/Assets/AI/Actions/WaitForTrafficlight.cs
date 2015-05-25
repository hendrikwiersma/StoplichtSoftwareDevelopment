using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class WaitForTrafficlight : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai) {

		Intersection intersection = ai.WorkingMemory.GetItem<Intersection> ("LatestIntersection");
		string direction = ai.WorkingMemory.GetItem<string> ("Direction");

		bool isClear = intersection.getLightState (direction);

		if (isClear) {

			return ActionResult.SUCCESS;

		} else {

			return ActionResult.FAILURE;
		
		}

    }

}