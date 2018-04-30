using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour {

	public static int birdCount = 30;
	public static int wayPoint = 10;

	public int birdSpawnRadius = 20;

	public GameObject Bird;

	public static GameObject[] allAnimal = new GameObject[birdCount];
	
	// Use this for initialization
	void Start () 
	{
		Spawn();
	}

	void Spawn()
	{
		for (int i = 0; i < birdCount; i++)
		{
			Vector3 c = Random.insideUnitCircle * birdSpawnRadius;
			Vector3 ranCoord = new Vector3 (c.x, c.z, c.y);

			allAnimal[i]= (GameObject) Instantiate (Bird, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
		}
	}
}
