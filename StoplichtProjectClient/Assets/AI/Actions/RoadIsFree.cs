using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RoadIsFree : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai) {

		// Load AI data
		string direction = ai.WorkingMemory.GetItem<string> ("Direction");
		Intersection currentIntersection = ai.WorkingMemory.GetItem<Intersection> ("PrevIntersection");
		Intersection nextIntersection = ai.WorkingMemory.GetItem<Intersection> ("Intersection");

		// Get the actual direction for prev intersection
		string curDirection = currentIntersection.findUsedDirection (direction);
		
		// Get the actual direction for this intersection
		string nextDirection = nextIntersection.findUsedDirection (direction);

		// Check if capacity is unlimited
		if (nextIntersection.getBikersMaxLane (nextDirection) != 0) {

			// Check if capacity is exceeded
			if (nextIntersection.getBikersMaxLane (nextDirection) <= nextIntersection.getBikersInLane(nextDirection)) {
				
				return ActionResult.FAILURE;
				
			}

		}

		// Get Lightstate
		bool lightIsGreen = currentIntersection.getLightState (curDirection);

		if (!lightIsGreen) {

			return ActionResult.FAILURE;
			
		}

        return ActionResult.SUCCESS;

    }

}