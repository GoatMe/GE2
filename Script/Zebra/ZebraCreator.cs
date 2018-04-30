using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCreator : MonoBehaviour {

	public static int targetCount = 50;

	public float radius = 30;

	public GameObject Zebra;
	public GameObject ZebraLeader;


	public static GameObject[] allZebra = new GameObject[targetCount];

	// Use this for initialization
	void Start () {
		
		Spawn();			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn()
	{ 
		GameObject a = GameObject.Find("SceneMaster");
		SceneControl sc;
			sc = a.GetComponent<SceneControl>();

		//Debug.Log("Array length: " +allZebra.Length);
		Transform parent = ZebraLeader.transform;
		for (int i = 0; i < 1; i++)
		{
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			allZebra[i] =(GameObject) Instantiate (ZebraLeader, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
			
			//get the previous position from SceneController if new scene is loaded
			if(!sc.allPos.Count.Equals(0))
			{
				allZebra[i].transform.position = sc.allPos[i];
			}
		}

		//Instantiate Object and assign them to be a child of a parent.
		for (int i = 1; i < targetCount; i++)
		{
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			allZebra[i] = (GameObject) Instantiate (Zebra, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
			allZebra[i].transform.SetParent (allZebra[0].transform); //set objects as childs to the leader object
			
			//get the previous position from SceneController if new scene is loaded
			if(!sc.allPos.Count.Equals(0))
			{
				allZebra[i].transform.position = sc.allPos[i];
			}
		}

		//check if any instantiated object is below terrain.
		//Adjust the object to be above the terrain by 0.5f.
		for(int i=0; i <= (targetCount - 1); i++)
		{
			Vector3 pos = allZebra[i].transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight(allZebra[i].transform.position) + 0.5f;
			pos.y -= 160.3f; // because terrain had been offset on Y-axis by -160.3
			allZebra[i].transform.position = pos;
		}
	}
}
