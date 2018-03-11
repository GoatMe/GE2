using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour {

	bool turning = false;

	public float speed = 10.0f;
	public float rotationSpeed = 4.0f;
	public float neighbourDist = 10.0f; //maximum distance for the object to flock to each other.

	Vector3 averageHeading;
	Vector3 averagePosition;


	// Use this for initialization
	void Start () {
		speed = Random.Range (0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance (transform.position, Vector3.zero) >= MainBehaviour.areaSize) {
			turning = true;	
		} else {
			turning = false;
		}

		if (turning) 
		{
			Vector3 direction = Vector3.zero - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (direction),
				rotationSpeed * Time.deltaTime);
			speed = Random.Range (0.5f, 1.5f);
		} else 
		{
			//control how often the flocking rule is applyed
			if(Random.Range(0,5) < 3)
			{
				ApplyRules ();
			}
		}

		if(Random.Range(0,5) < 3)
		{
			ApplyRules ();
		}
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyRules()
	{
		GameObject[] gos;

		gos = new GameObject[ZebraCreator.targetCount];
		gos = GameObject.FindGameObjectsWithTag("Zebra");
		Vector3 vCentre = Vector3.zero;
		Vector3 vAvoid = Vector3.zero;

		float groupSpeed = 0.1f;

		Vector3 goalPos = MainBehaviour.goalPos;

		float dist;

		int groupSize = 0; //calc group size based on who's withing the groupDistance

		foreach(GameObject go in gos)
		{
			if (go != this.gameObject) 
			{
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighbourDist)
				{
					vCentre += go.transform.position; //add all group centers
					groupSize++;	//add groupSize

					//if too close then start avoiding and calc vector to move other direction.
					if (dist < 3.0f) 
					{
						vAvoid = vAvoid + (this.transform.position - go.transform.position);
					}

					//find the average speed of the entire group - by adding all speed of the entire flock
					Flocking anotherFlock = go.GetComponent<Flocking> ();
					groupSpeed = groupSpeed + anotherFlock.speed;
				}
			}
		}

		//if object is in a group - then cal the average center & speed of the group
		if (groupSize > 0)
		{
			vCentre = vCentre / groupSize + (goalPos - this.transform.position);
			speed = groupSpeed / groupSize;

			Vector3 direction = (vCentre + vAvoid) - transform.position;
			if (direction != Vector3.zero) 
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, 
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);
			}
		}
	}

}
