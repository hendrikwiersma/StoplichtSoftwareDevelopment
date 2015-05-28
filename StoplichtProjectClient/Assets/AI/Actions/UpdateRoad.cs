 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class UpdateRoad : RAINAction
{

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		// Load AI data
		string direction = ai.WorkingMemory.GetItem<string> ("Direction");
		Intersection prevIntersection = ai.WorkingMemory.GetItem<Intersection> ("PrevIntersection");
		Intersection currIntersection = ai.WorkingMemory.GetItem<Intersection> ("Intersection");

		// Get the used direction for this intersection
		string prevDirection = prevIntersection.findUsedDirection (direction);
		string currDirection = currIntersection.findUsedDirection (direction);

		// Set bikers amount
		prevIntersection.bikersInLane[dir (prevDirection)] -= 1 ;
		currIntersection.bikersInLane[dir (currDirection)] += 1 ;

        return ActionResult.SUCCESS;
    }

	private int dir (string direction) {

		switch(direction) {

		case "North": return 0;
		case "East": return 1;
		case "South": return 2;
		case "West": return 3;

		}

		return 0;

	}

}