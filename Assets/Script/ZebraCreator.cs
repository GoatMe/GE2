using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCreator : MonoBehaviour {

	public static int targetCount = 10;

	public float radius = 30;

	public GameObject Zebra;
	public GameObject ZebraLeader;


	public GameObject[] allZebra = new GameObject[targetCount];

	// Use this for initialization
	void Start () {
		Instantiate (ZebraLeader, 
			transform.position, 
			Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);

		Spawn();			
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Spawn()
	{
		for (int i = 0; i < targetCount; i++)
		{
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			allZebra[i]= (GameObject) Instantiate (Zebra, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
		}

	}
}
