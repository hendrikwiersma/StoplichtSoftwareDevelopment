// MyScript.cs
using UnityEngine;

public class MyScript : MonoBehaviour {
	
	[SerializeField]
	private int myValue;
	
	// property is not used by custom editor,
	// but is used to ensure valid values from other scripts
	public int MyValue {
		get { return myValue; }
		set {
			// MyValue should be between 1 and 10
			myValue = Mathf.Clamp(MyValue, 1, 10);
		}
	}
	
	void Update () {
		// do something with MyValue
	}
}