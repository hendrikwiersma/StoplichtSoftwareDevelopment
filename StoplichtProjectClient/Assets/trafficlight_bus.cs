using UnityEngine;
using System.Collections;

public class trafficlight_bus : MonoBehaviour {
	public GameObject bulb;
	public Texture rechtdoor;
	public Texture linksaf;
	public Texture rechtsaf;
	public Texture allerichtingen;
	public Texture stopmogelijk;
	public Texture stop;

	private int counter = 0;
	private ArrayList textures = new ArrayList();
	private float nextActionTime = 0.0f; public float period = 0.1f;
	// Use this for initialization
	void Start () {
		textures.Add (rechtdoor);
		textures.Add (linksaf);
		textures.Add (rechtsaf);
		textures.Add (allerichtingen);
		textures.Add (stopmogelijk);
		textures.Add (stop);
	}

	public void switchlight(string state){
		if (state == "rechtdoor") {
			bulb.GetComponent<Renderer> ().material.mainTexture = rechtdoor;
		} else if (state == "linksaf") {
			bulb.GetComponent<Renderer> ().material.mainTexture = linksaf;
		} else if (state == "rechtsaf") {
			bulb.GetComponent<Renderer> ().material.mainTexture = rechtsaf;
		}
		else if (state == "allerichtingen") {
			bulb.GetComponent<Renderer> ().material.mainTexture = allerichtingen;
		}
		else if (state == "stopmogelijk") {
			bulb.GetComponent<Renderer> ().material.mainTexture = stopmogelijk;
		}
		else if (state == "stop") {
			bulb.GetComponent<Renderer> ().material.mainTexture = stop;
		}
		else {
			print ("Invalid traffic light state.");
		}
	}
	void Update () 
	{
		if (Time.time > nextActionTime) { 
			nextActionTime += period;
			if(counter >= textures.Count){
				counter = 0;
			}
			print ("Changing texture");
			bulb.GetComponent<Renderer> ().material.mainTexture = (Texture)textures[counter];
			counter++;
		}
	}
}
