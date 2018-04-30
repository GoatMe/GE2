using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsPathFollow : MonoBehaviour {

	private GameObject target;
	public float waypointRadius = 1f;
	public float damping = 0.1f;
	public float movespeed =0.1f;
	public float rotationSpeed = 1.0f;
	
	public int index = 0;

	public List<GameObject> BirdWayPointList = new List<GameObject>();


	// Use this for initialization
	void Start ()
	{
		//get all waypoints
		if(BirdWayPointList.Count.Equals(0))
		{
		BirdWayPointList.AddRange(GameObject.FindGameObjectsWithTag("BirdWayPoint"));
			
		}

		//get a random starter point
		Vector3 direction = BirdWayPointList[index].transform.position - transform.position;
		direction.y = 0;
	}
	void Update()
	{
		PathFollow ();
	}

	public void PathFollow()
	{
		Vector3 direction = BirdWayPointList[index].transform.position - transform.position;
		direction.y = 0;
		float distance = direction.magnitude;

		if (distance <= waypointRadius) 
		{
			if( index < BirdWayPointList.Count)
			{
			index += 1;
			}
			if( index == BirdWayPointList.Count)
			{
				index = 0;
			}
		} 
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * movespeed;
		transform.position += moveVector;
	}
}