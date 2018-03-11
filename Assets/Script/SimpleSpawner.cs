using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour {

	static int targetCount = 20;
	public float radius = 30;

	public GameObject meerkatSpawn;

	public GameObject zebraTarget;
	private GameObject zebraGoal;

	public static GameObject[] allAnimal = new GameObject[targetCount];

	// Use this for initialization
	void Start ()
	{
		if (zebraGoal == null) {
			zebraGoal = GameObject.FindWithTag ("Finish");
			Debug.Log (zebraGoal);
		}
		if (zebraTarget == null){
			zebraTarget = GameObject.Find("ZebraSeek").gameObject;
			Debug.Log (zebraTarget);
		}
		Instantiate (zebraTarget, zebraGoal.transform.position, zebraGoal.transform.rotation);

		Spawn();
	}

	// Update is called once per frame
	void Update ()
	{

	}


	void Spawn()
	{

		for (int i = 0; i < targetCount; i++) {
			Vector2 c = Random.insideUnitCircle * radius;
			Vector3 ranCoord = new Vector3 (c.x, 0, c.y);
			allAnimal[i]= (GameObject) Instantiate (meerkatSpawn, 
				transform.position + ranCoord, 
				Quaternion.AngleAxis (Random.Range (0, 360), transform.up) * transform.rotation);

		}
	}
}
