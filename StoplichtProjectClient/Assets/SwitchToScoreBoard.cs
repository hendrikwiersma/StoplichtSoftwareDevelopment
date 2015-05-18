using UnityEngine;
using System.Collections;

public class SwitchToScoreBoard : MonoBehaviour {
	public GameObject PlayerScoreboardPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			transform.parent = null;
			transform.position = PlayerScoreboardPos.transform.position;
		}
	}
}
