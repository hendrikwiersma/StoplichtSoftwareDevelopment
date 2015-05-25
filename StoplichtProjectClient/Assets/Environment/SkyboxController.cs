using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {
	public GameObject SkyboxCube;
	public Material Skybox1;
	public Material Skybox2;
	public Material Skybox3;
	public Material Skybox4;
	public Material Skybox5;
	public GameObject CubeSkybox1;
	public GameObject CubeSkybox2;
	public GameObject CubeSkybox3;
	public GameObject CubeSkybox4;
	public GameObject CubeSkybox5;
	Color fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);
    Color fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);
    public GameObject User;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)){
			RenderSettings.skybox = Skybox1;
			Destroy(SkyboxCube);
			SkyboxCube = Instantiate(CubeSkybox1, new Vector3(0,0,0), Quaternion.Euler(-90,0,0)) as GameObject;
			skybox_rotate rotatescript = SkyboxCube.GetComponent<skybox_rotate>();
			rotatescript.User = User;
			SkyboxCube.transform.parent = transform;
		}
		else if (Input.GetKeyDown(KeyCode.F2)){
			RenderSettings.skybox = Skybox2;
			Destroy(SkyboxCube);
			SkyboxCube = Instantiate(CubeSkybox2, new Vector3(0,0,0), Quaternion.Euler(-90,0,0)) as GameObject;
			skybox_rotate rotatescript = SkyboxCube.GetComponent<skybox_rotate>();
			rotatescript.User = User;
			SkyboxCube.transform.parent = transform;
		}
		else if (Input.GetKeyDown(KeyCode.F3)){
			RenderSettings.skybox = Skybox3;
			Destroy(SkyboxCube);
			SkyboxCube = Instantiate(CubeSkybox3, new Vector3(0,0,0), Quaternion.Euler(-90,0,0)) as GameObject;
			skybox_rotate rotatescript = SkyboxCube.GetComponent<skybox_rotate>();
			rotatescript.User = User;
			SkyboxCube.transform.parent = transform;
		}
		else if (Input.GetKeyDown(KeyCode.F4)){
			RenderSettings.skybox = Skybox4;
			Destroy(SkyboxCube);
			SkyboxCube = Instantiate(CubeSkybox4, new Vector3(0,0,0), Quaternion.Euler(-90,0,0)) as GameObject;
			skybox_rotate rotatescript = SkyboxCube.GetComponent<skybox_rotate>();
			rotatescript.User = User;
			SkyboxCube.transform.parent = transform;
		}
		else if (Input.GetKeyDown(KeyCode.F5)){
			RenderSettings.skybox = Skybox5;
			Destroy(SkyboxCube);
			SkyboxCube = Instantiate(CubeSkybox5, new Vector3(0,0,0), Quaternion.Euler(-90,0,0)) as GameObject;
			skybox_rotate rotatescript = SkyboxCube.GetComponent<skybox_rotate>();
			rotatescript.User = User;
			SkyboxCube.transform.parent = transform;
		}
	}
}
