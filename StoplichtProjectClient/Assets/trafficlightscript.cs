using UnityEngine;
using System.Collections;

public class trafficlightscript : MonoBehaviour {
	public int ID;
	public GameObject roodlicht;
	public GameObject oranjelicht;
	public GameObject groenlicht;
	public Shader uitshader;
	public Shader aanshader;

	// Use this for initialization
	void Start () {
		roodlicht.GetComponent<Renderer> ().material.shader = aanshader;
	}
	public void switchlight(string lightcolor){
		print ("Received the color: " + lightcolor + " on the id " + ID);
		if (lightcolor == "Rood") {
			roodlicht.GetComponent<Renderer> ().material.shader = aanshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = uitshader;
			groenlicht.GetComponent<Renderer> ().material.shader = uitshader;
		} else if (lightcolor == "Oranje") {
			roodlicht.GetComponent<Renderer> ().material.shader = uitshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = aanshader;
			groenlicht.GetComponent<Renderer> ().material.shader = uitshader;
		} else if (lightcolor == "Groen") {
			roodlicht.GetComponent<Renderer> ().material.shader = uitshader;
			oranjelicht.GetComponent<Renderer> ().material.shader = uitshader;
			groenlicht.GetComponent<Renderer> ().material.shader = aanshader;
		} else {
			print ("Invalid traffic light color.");
		}
	}

}
