using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FreezeObject : RAINAction
{

	// Freeze bicycle on spot
    public override ActionResult Execute(RAIN.Core.AI ai) {

		Rigidbody r = ai.Body.GetComponent<Rigidbody> ();
		r.constraints = RigidbodyConstraints.FreezeAll;
        return ActionResult.FAILURE;

    }

}