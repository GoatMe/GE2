using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour {


Vector3 velocity; //the speed of something in a given direction.
	Vector3 pos;
	Vector3 accleleration;
	Vector3 force;

	public float mass = 1f;

	public GameObject seekTarget;

	[Range(0.0F, 100.0F)][Tooltip("Movement Speed of the Object")]
	public float moveSpeed =1f;

//	[Range(0.0F, 10.0F)][Tooltip("Reduce the force which causes the overshooting")]
//	public float damping = 0.1f;

	[Range(0.0F, 100.0F)][Tooltip("Max Speed to which the Object can Accelerato to")]
	public float maxSpd = 10.0f;

//	private Vector3 currentTrajectory;
//	private Vector3 targetTrajectory;


	public float rotationSpeed = 1.0f;
	public float minDistance = 5.0f;
	public float safeDistance = 10.0f;

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, seekTarget.transform.position);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * 20);
	}

	// Use this for initialization
	void Start () {
		//velocity = Vector3.zero;

		//moves the GameObject while also considering its rotation.
		//currentTrajectory = transform.forward;
		if(seekTarget == null)
		{
		seekTarget = GameObject.FindGameObjectWithTag("LeaderZebra");
			
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		Seeking ();
	}
//
//	// calculates a new heading
//	protected void FixedUpdate ()
//	{
//		targetTrajectory = seekTarget.transform.position - transform.position;
//
//		currentTrajectory = Vector3.Lerp(currentTrajectory,targetTrajectory,damping*Time.deltaTime);
//	}

	public void Seeking()
	{
		//calculate objects velocity(speed)
		Vector3 desired = seekTarget.transform.position - transform.position;
		desired.Normalize();
		desired  *= maxSpd;

		force = desired - velocity;

		accleleration = force / mass;

		velocity += accleleration * Time.deltaTime;

		transform.position += velocity*Time.deltaTime;
		///epsilon - smallest value for a float
		//rotation towards way object is moving
		if(velocity.magnitude > float.Epsilon)
		{
			Vector3 direction  = seekTarget.transform.position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		}
	}
}
