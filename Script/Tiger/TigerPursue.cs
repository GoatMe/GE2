using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TigerPursue : MonoBehaviour {

	public float moveSpeed = 1.5f;
	public float rotationSpeed = 1.0f;
	public float minDistance = 5.0f;

	Vector3 velocity; //the speed of something in a given direction.
	Vector3 pos;
	Vector3 accleleration;
	Vector3 force;
	public Vector3 direction;
	public float gravity = 1f;

	public List<float> allZebraDist = new List<float>();
	public List<GameObject> allPrefabsPos;
	public List<Vector3> allPos = new List<Vector3>();

	private int index;

	Animator anim;
	int running = Animator.StringToHash("Run");
	int walking = Animator.StringToHash("Walk");
	int idle = Animator.StringToHash("Idle");

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

		allPrefabsPos = new List<GameObject>(ZebraCreator.allZebra);
		Target();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Debug.Log("index: "+index);
		if (allPos[index].magnitude >= 5 && allPos[index].magnitude <= 30)
		{
			Target();
		}

		Pursuit();
	}

	void Pursuit()
	{
		Vector3 speed;
		Vector3 target = ZebraCreator.allZebra[index].transform.position;
		Vector3 desired = target - transform.position;
		desired.Normalize();
		desired  *= moveSpeed;

		force = desired - velocity;

		accleleration = force / gravity;

		velocity += accleleration * Time.deltaTime;

		transform.position += velocity*Time.deltaTime;

		int iterationAhead = 30;
		Vector3 targetSpeed = desired;
		Vector3 targetFuturePosition = target + (targetSpeed * iterationAhead);
		direction = targetFuturePosition - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		// Debug.Log("Distance: "+direction.magnitude+" - minDistance: "+minDistance);
		if(direction.magnitude >= minDistance)
		{
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		}
		speed = direction.normalized * moveSpeed * Time.deltaTime;

		if (speed.magnitude <= 0)
		{
			anim.SetTrigger (idle);	
//			anim.SetFloat ("Speed", speed);
		} else if (speed.magnitude <= 0.02f) //less than 20%
		{
			anim.SetTrigger (walking);
//			anim.SetFloat ("Speed", speed);
		} else if (speed.magnitude >= 0.02f) //greater than 33%
		{
			anim.SetTrigger (running);	
//			anim.SetFloat("Speed", speed);
		}
	}

	void Target()
	{
		//find the closest target to follow, bases on distance between
		for (int i = 0; i < allPrefabsPos.Count; i++)
		{
			//Debug.Log(allPrefabsPos[i].transform.position);
			allPos.Add(allPrefabsPos[i].transform.position);
			allZebraDist.Add(allPos[i].magnitude);
		}
		index = allZebraDist.IndexOf(allZebraDist.Min()); //closest target
	}
}
