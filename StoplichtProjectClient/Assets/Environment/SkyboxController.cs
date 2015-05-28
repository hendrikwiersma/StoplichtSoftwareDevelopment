using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {
	public Material Skybox1;
	public Material Skybox2;
	public Material Skybox3;
	public Material Skybox4;
	public Material Skybox5;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)){
			RenderSettings.skybox = Skybox1;
		}
		else if (Input.GetKeyDown(KeyCode.F2)){
			RenderSettings.skybox = Skybox2;

		}
		else if (Input.GetKeyDown(KeyCode.F3)){
			RenderSettings.skybox = Skybox3;
		}
		else if (Input.GetKeyDown(KeyCode.F4)){
			RenderSettings.skybox = Skybox4;
		}
		else if (Input.GetKeyDown(KeyCode.F5)){
			RenderSettings.skybox = Skybox5;
		}
	}
}
