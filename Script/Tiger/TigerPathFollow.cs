using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerPathFollow : MonoBehaviour {

	private GameObject target;
	public float waypointRadius = 3f;
	public float damping = 0.1f;
	public float movespeed =0.1f;
	public float rotationSpeed = 1.0f;

	private int randomPnt; 

	private float totalDistance;

	public List<GameObject> TigerWayPointList = new List<GameObject>();

	Animator anim;
	int running = Animator.StringToHash("Run");
	int walking = Animator.StringToHash("Walk");
	int idle = Animator.StringToHash("Idle");

	//int runStateHash = Animator.StringToHash("Base Layer.Idle");

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		//get all waypoints
		if(TigerWayPointList.Count.Equals(0))
		{
		TigerWayPointList.AddRange(GameObject.FindGameObjectsWithTag("TigerWayPoint"));
			
		}

		//get a random starter point
		randomPnt = RandomPnt();
		Vector3 direction = TigerWayPointList[randomPnt].transform.position - transform.position;
		direction.y = 0;
		totalDistance = direction.magnitude;
	}
	void Update()
	{
		PathFollow ();
	}

	public void PathFollow()
	{
		float speed = 0f;
		float decelerationFactor = 0f;
		Vector3 direction = TigerWayPointList[randomPnt].transform.position - transform.position;
		direction.y = 0;
		float distance = direction.magnitude;

		float distancePercent = totalDistance / 100; 

		decelerationFactor = distance / distancePercent;

		if (distance <= waypointRadius) {
			randomPnt = RandomPnt();
		} else {
			speed = movespeed * decelerationFactor;
			//Debug.Log ("Move Speed "+movespeed+ " Speed " +speed);

		}
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * speed;
		transform.position += moveVector;

		if (speed <= 0)
		{
			anim.SetTrigger (idle);	
//			anim.SetFloat ("Speed", speed);
		} else if (speed <= (movespeed/5)) //less than 20%
		{
			anim.SetTrigger (walking);
//			anim.SetFloat ("Speed", speed);
		} else if (speed >= (movespeed/3)) //greater than 33%
		{
			anim.SetTrigger (running);	
//			anim.SetFloat("Speed", speed);
		}
	}

	public int RandomPnt ()
	{
		int rngPoint = Random.Range(0, TigerWayPointList.Count); 
		return rngPoint;
	}

}