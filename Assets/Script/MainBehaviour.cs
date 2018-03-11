using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBehaviour : MonoBehaviour {
	
	public static int areaSize = 100;
	static int targetCount = 100;

	int spwTtl = 0;
	//public int targetCount;
	public float radius = 30;

	public GameObject goalPrefab;
	public GameObject AnimalPrefab;

	public static GameObject[] allAnimal = new GameObject[targetCount];

	// position of an object towards which the animals will move.
	public static Vector3 goalPos = Vector3.zero; 

	public float spawnTime = 0f;
	public float spawnRate = 0f;

	// Use this for initialization
	void Start () {
//		for(int i = 0; i < numAnimal; i++)
//		{
//			//ToDo change the Instantiation...
//
//			//creates a position for the prefab randomly around the origin in the world
//			Vector3 pos = new Vector3(Random.Range(-packSize,packSize),
//										0, 
//										Random.Range(-packSize,packSize));
//			allAnimal[i]= (GameObject) Instantiate(animalPrefab, pos, Quaternion.identity);
//		}
//		Instantiate(goalPrefab, transform.position + goalPos, Quaternion.identity);
//		goalPos =  new Vector3(Random.Range(-areaSize,areaSize),
//			1, 
//			Random.Range(-areaSize,areaSize));
		goalPos = new Vector3(30,1,30);
		goalPrefab.transform.position = goalPos;

		Spawn();
//		InvokeRepeating ("Spawn", spawnTime, spawnRate);
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if(Random.Range(0,10000) < 100)
//		{

//		}	
	}


	void Spawn(){

		for (int i = 0; i < targetCount; i++) {
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);
			//		transform.rotation = Quaternion.AngleAxis(Random.Range(0,360), transform.up) * transform.rotation;

			allAnimal[i]= (GameObject) Instantiate (AnimalPrefab, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);

		}
//		GameObject[] Zebras = GameObject.FindGameObjectsWithTag("Zebra");
////		Debug.Log (Zebras.Length);
//		if (Zebras.Length == targetCount) {
//			CancelInvoke ("Spawn");
//		}
	}

}
