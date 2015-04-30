using UnityEngine;
using System.Collections;

public class moveAgent : MonoBehaviour {

	private NavMeshAgent agent;
	private Vector3 destination;
	public Component dest;

	// Use this for initialization
	void Start () {
	
		agent = GetComponent<NavMeshAgent>();
		//destination = agent.destination;
		agent.destination = dest.transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
	


	}
}
