using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraMainFlockController : MonoBehaviour {

	public Vector3 offset;
	public Vector3 bound;
	public float speed = 10.0f;
	private Vector3 initialPosition;
	private Vector3 EndPoint;
	private Vector3 direction;
	public float decelerationFactor = 0f;
	public float distance = 0f;
	public float rotationSpeed = 1.0f;

	private float totalDistance;

	private float mSpeed;

	Animator anim;
	int running = Animator.StringToHash("Run");
	int walking = Animator.StringToHash("Walk");
	int graze = Animator.StringToHash("Graze");

	// int runStateHash = Animator.StringToHash("Base Layer.Idle");


	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, EndPoint);
	}

// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		// Debug.Log("Child Count: " + transform.childCount);
		initialPosition = transform.position;
		initialPosition.y = 0;
		EndPoint = GameObject.FindGameObjectWithTag("Finish").transform.position;
		//Debug.Log ("Move Point "+EndPoint);

		direction = EndPoint - transform.position;
		direction.y =0;
		float distance = direction.magnitude;
		totalDistance = distance;

		mSpeed = speed;
	}
	// Update is called once per frame
	void Update ()
	{
		Arrival();
	}

	public void Arrival()
	{
		direction = EndPoint - transform.position;
		direction.y =0;
		distance = direction.magnitude;
		//Debug.Log("distance "+distance);
		float distancePercent = totalDistance / 100; 
		//Debug.Log("speed" +speed);
		decelerationFactor = distance / distancePercent;
		//Debug.Log("decelerationFactor "+decelerationFactor);
		if (distance <= 3) {
			speed = 0;
		} 
		else if(distance <= 15)
		{
			if(speed > 1)
			{
				speed = speed - 0.1f;
				//Debug.Log ("1-Move Speed "+mSpeed+ " Speed " +speed);	
			}
		}
		else
		{
			speed = mSpeed;
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(EndPoint - transform.position), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * speed;
		transform.position += moveVector;

		if (distance <= 3)
		{
			anim.SetTrigger (graze);	
//			anim.SetFloat ("Speed", speed);
		} else if (distance <= 15) 
		{
			anim.SetTrigger (walking);
//			anim.SetFloat ("Speed", speed);
		} else 
		{
			anim.SetTrigger (running);	
//			anim.SetFloat("Speed", speed);
		}
	}
}
