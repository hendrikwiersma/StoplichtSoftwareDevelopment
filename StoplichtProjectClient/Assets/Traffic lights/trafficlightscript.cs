using UnityEngine;
using System.Collections;

public class trafficlightscript : MonoBehaviour {
	public int ID;
	public GameObject roodlicht;
	public GameObject oranjelicht;
	public GameObject groenlicht;
	public Shader uitshader;
	public Shader aanshader;
	
	void Start () {

		roodlicht.GetComponent<Renderer> ().material.shader = aanshader;

	}

	public void switchlight(Data.LIGHT_STATE state){

		switch(state) {

		case Data.LIGHT_STATE.RED:
			roodlicht.GetComponent<Renderer> ().material.shader = aanshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = uitshader;
			groenlicht.GetComponent<Renderer> ().material.shader = uitshader;
			break;

		case Data.LIGHT_STATE.ORANGE:
			roodlicht.GetComponent<Renderer> ().material.shader = uitshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = aanshader;
			groenlicht.GetComponent<Renderer> ().material.shader = uitshader;
			break;

		case Data.LIGHT_STATE.GREEN:
			roodlicht.GetComponent<Renderer> ().material.shader = uitshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = uitshader;
			groenlicht.GetComponent<Renderer> ().material.shader = aanshader;
			break;

		default:
			print ("Invalid traffic light color.");
			break;

		}

	}

}
