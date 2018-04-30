using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraControl : MonoBehaviour {

	public float moveSpeed = 20.0f;

	private GameObject target;
	public float waypointRadius = 15f;
	public float damping = 0.1f;
	public float movespeed =0.5f;
	public float rotationSpeed = 1.0f;

	private float totalDistance;

	Animator anim;
	int idle = Animator.StringToHash("Idle");
	int running = Animator.StringToHash("Run");
	int walking = Animator.StringToHash("Walk");

	// int runStateHash = Animator.StringToHash("Base Layer.Idle");

	void Start ()
	{
		if (target == null){
			target = GameObject.FindWithTag("Finish");
			//Debug.Log (target);
		}

		anim = GetComponent<Animator>();

		Vector3 direction = target.transform.position - transform.position;
		direction.y = 0;
		totalDistance = direction.magnitude;
	}
		
	void Update ()
	{
		// if (target == null){
		// 	target = GameObject.FindWithTag("ZebraTarget");
		// 	//Debug.Log (target);
		// }

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

		//AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (distance <= 5) {
			speed = 0;

		} else{
			speed = movespeed * decelerationFactor;
			Debug.Log ("Move Speed "+movespeed+ " Speed " +speed);

		}
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * speed;
		transform.position += moveVector;	

		if (distance <= 5)
		{
			anim.SetTrigger (idle);	
//			anim.SetFloat ("Speed", speed);
		} else if (distance <= 30) 
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