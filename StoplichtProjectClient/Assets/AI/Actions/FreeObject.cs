using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FreeObject : RAINAction {

	// Allow bicycle to move
	public override ActionResult Execute(RAIN.Core.AI ai) {
		
		Rigidbody r = ai.Body.GetComponent<Rigidbody> ();
		r.constraints = RigidbodyConstraints.None;
		return ActionResult.SUCCESS;
		
	}

}