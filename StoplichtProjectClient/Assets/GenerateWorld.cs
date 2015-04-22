using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	public GameObject road1;
	public GameObject road2;
	public GameObject grass1;
	public GameObject grass2;
	public GameObject obstacle;
	public GameObject tree;
	public GameObject intersection;
	// Use this for initialization
	void Start () {
		for(int i = 0;i<200;i++){
			for(int j = 0;j<200;j++){
				int bewl = Random.Range(1, 20);
				int oneurtwo = Random.Range(1, 6);
				if(oneurtwo == 1){
					Instantiate(road1, new Vector3(i*10,0,j*10), road1.transform.rotation);
				}
				else if(oneurtwo == 2){
					Instantiate(grass1, new Vector3(i*10,0,j*10), grass1.transform.rotation);
				}
				else if(oneurtwo == 3){
					Instantiate(grass2, new Vector3(i*10,0,j*10), grass2.transform.rotation);
				}
				else if (oneurtwo == 4){
					Instantiate(road2, new Vector3(i*10,0,j*10), road2.transform.rotation);
				}
				else{
					Instantiate(intersection, new Vector3(i*10,0,j*10), intersection.transform.rotation);
				}
				if(bewl == 1.0){
					GameObject obj = Instantiate(obstacle, new Vector3(i*10,0,j*10), road1.transform.rotation) as GameObject;
					obj.transform.localScale = new Vector3(10, 10, 50*bewl);
				}
				else if(bewl == 2.0){
					Instantiate(tree, new Vector3(i*10,4,j*10), tree.transform.rotation);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
