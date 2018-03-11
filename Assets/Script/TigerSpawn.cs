using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSpawn : MonoBehaviour {

	public static int targetCount = 2;

	public float radius = 30;

	public GameObject Tiger;

	public static GameObject[] allAnimal = new GameObject[targetCount];

	// Use this for initialization
	void Start () 
	{
		Spawn();
	}

	void Spawn(){


		for (int i = 0; i < targetCount; i++) {
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			allAnimal[i]= (GameObject) Instantiate (Tiger, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);

		}

	}

}
