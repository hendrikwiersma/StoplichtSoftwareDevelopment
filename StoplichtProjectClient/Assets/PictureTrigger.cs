using UnityEngine;
using System.Collections;

public class PictureTrigger : MonoBehaviour {
	public GameObject display;
	public Camera screenshotcamera;
	public GameObject ScreenShotsOriginPoint;
	public int resWidth = 1024;
    public int resHeight = 768;
    private int xOffset = 0;
    private int zOffset = 0;
	void OnCollisionEnter(Collision collision) {
		Material newmaterial = new Material (Shader.Find("Standard"));
		GameObject newdisplay = Instantiate(display, ScreenShotsOriginPoint.transform.position, Quaternion.Euler(90, 90, 0)) as GameObject;
		newdisplay.transform.parent = ScreenShotsOriginPoint.transform;

		newdisplay.transform.localPosition = new Vector3(xOffset, 0, zOffset);
		zOffset = zOffset - 15;
		if(zOffset <= -100){
			xOffset = xOffset - 12;
			zOffset = 0;
		}

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        screenshotcamera.targetTexture = rt; 
        Texture2D screenShot = new Texture2D(resWidth, resHeight, 
                                             TextureFormat.RGB24, false);
        screenshotcamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        
        screenshotcamera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors

        Destroy(rt);
        screenShot.Apply();
        newmaterial.mainTexture = screenShot;
    	newdisplay.GetComponent<Renderer> ().material = newmaterial;
	}
}