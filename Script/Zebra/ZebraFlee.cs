using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraFlee : MonoBehaviour {

	public GameObject persuer;

	public float safeDistance = 15.0f;
	public float rotationSpeed = 1f;
	public float moveSpeed = 6.0f;

	public Vector3	velocity;

	// Use this for initialization
	void Start () 
	{
		if(persuer == null)
		{
		persuer = GameObject.FindGameObjectWithTag("Tiget");
			
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		FleeBehaviour ();
	}

	void FleeBehaviour()
	{
		Vector3 direction  = transform.position - persuer.transform.position;
		direction.y = 0;

		//Debug.Log("Distance: "+direction.magnitude+" - safeDistance: "+safeDistance);
		
		if(direction.magnitude <= safeDistance)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, 
								Quaternion.LookRotation(direction), 
								rotationSpeed * Time.deltaTime);
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		}
	}
}