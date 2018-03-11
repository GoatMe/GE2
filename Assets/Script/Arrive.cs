using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

	private GameObject target;
	public float waypointRadius = 15f;
	public float damping = 0.1f;
	public float movespeed =0f;
	public float rotationSpeed = 1.0f;

	private float totalDistance;


	// Use this for initialization
	protected void Start ()
	{
		if(target == null)
		{
		target = GameObject.FindGameObjectWithTag("ZebraTarget");
			
		}

		Vector3 direction = target.transform.position - transform.position;
		direction.y = 0;
		totalDistance = direction.magnitude;
	}



	// moves us along current heading
	protected void Update()
	{
		//target = GameObject.Find ("Finish").gameObject;

		Arrival ();
	}

	public void Arrival()
	{
		float speed = 0f;
		float decelerationFactor = 0f;
		Vector3 direction = target.transform.position - transform.position;
		direction.y = 0;
		float distance = direction.magnitude;

		float distancePercent = totalDistance / 100; 

		decelerationFactor = distance / distancePercent;

		if (distance <= 1) {
			speed = 0;
		} else {
			speed = movespeed * decelerationFactor;
			Debug.Log ("Move Speed "+movespeed+ " Speed " +speed);

		}
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * speed;
		transform.position += moveVector;
	}

}
