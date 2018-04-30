using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSpawn : MonoBehaviour {

	public static int tigerCount = 5;
	public static int wayPoint = 10;


	public GameObject Tiger;

	public static GameObject[] allAnimal = new GameObject[tigerCount];
	
	public int tigerSpawnRadius = 30;
	public int wayPointSpawnRadius = 30;

	public GameObject tigerWayPoint;
	
	public static GameObject[] tigerWaypoints = new GameObject[wayPoint];

	// Use this for initialization
	void Start () 
	{
		Spawn();
		WayPointSpawn();
	}

	void Spawn()
	{
		for (int i = 0; i < tigerCount; i++)
		{
			Vector2 c = Random.insideUnitCircle * tigerSpawnRadius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			allAnimal[i]= (GameObject) Instantiate (Tiger, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
		}

		for(int i=0; i <= (tigerCount - 1); i++)
		{
			Vector3 pos = allAnimal[i].transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight(allAnimal[i].transform.position) + 0.5f;
			pos.y -= 160.3f; // because terrain had been offset on Y-axis by -160.3
			allAnimal[i].transform.position = pos;
		}
	}

	void WayPointSpawn()
	{
		for (int i = 0; i < wayPoint; i++) 
		{
			Vector2 c = Random.insideUnitCircle * wayPointSpawnRadius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);

			tigerWaypoints[i]= (GameObject) Instantiate (tigerWayPoint, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);
		}

		for(int i=0; i <= (wayPoint - 1); i++)
		{
			Vector3 pos = tigerWaypoints[i].transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight(tigerWaypoints[i].transform.position) + 1f;
			pos.y -= 160.3f; // because terrain had been offset on Y-axis by -160.3
			tigerWaypoints[i].transform.position = pos;
		}
	}

}
