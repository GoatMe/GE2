using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraFlock : MonoBehaviour {

	public float minSpeed = 20.0f;
	public float turnSpeed = 20.0f;
	public float randomFreq = 20.0f;
	public float randomForce = 20.0f;
	
	//alignment variables
	public float toOriginForce = 50.0f; 
	public float toOriginRange = 50.0f;
	public float gravity = 2.0f;
	
	//seperation variables
	public float avoidanceRadius = 50.0f;
	public float avoidanceForce = 20.0f;
	
	//cohesion variables
	public float followVelocity = 4.0f;
	public float followRadius = 40.0f;
	
	//these variables control the movement of the boid
	private Transform origin;
	private Vector3 velocity;
	private Vector3 normalizedVelocity;
	private Vector3 randomPush;
	private Vector3 originPush;
	private Transform[] objects;
	private ZebraFlock[] otherFlocks;
	private Transform transformComponent;

	private bool flock = true;
	
	private Vector3 EndPoint;
	
	public float distance;
	public float ranDistStop;
	// Use this for initialization
	void Start () 
	{
		randomFreq = 1.0f / randomFreq;
		//Assign the parent as origin
		origin = transform.parent;
		//Flock transform
		transformComponent = transform;
		
		//Temporary components
		Component[] tempFlocks= null;
		
		//Get all the unity flock components from the parent transform in the group
		if (transform.parent) 
		{
		      tempFlocks = transform.parent.GetComponentsInChildren<ZebraFlock>();
		}
		//Assign and store all the flock objects in this group
		objects = new Transform[tempFlocks.Length];
		otherFlocks = new ZebraFlock[tempFlocks.Length];
		
		for (int i = 0;i<tempFlocks.Length;i++)
		{
			objects[i] = tempFlocks[i].transform;
			otherFlocks[i] = (ZebraFlock)tempFlocks[i];
		}
		//Null Parent as the flock leader will be 
		//ZebraMainFlockController object
		transform.parent = null;
		//Calculate random push depends on the random frequency provided
		ranDistStop = Random.Range(3.0f, 10.0f);
		StartCoroutine(UpdateRandom());
	}

	// Update is called once per frame
	void Update () 
	{  //Internal variables
		if(flock)
		{
			Flock();
		}

		EndPoint = GameObject.FindGameObjectWithTag("Finish").transform.position;
		Vector3 direction = EndPoint - transform.position;
		direction.y =0;
		distance = direction.magnitude;

		

		if( distance <= ranDistStop)
		{ 
			flock = false;
		}
		else if( distance > ranDistStop)
		{
			flock =true;
		}
	}

	void Flock()
	{
		float speed = velocity.magnitude;
		Vector3 avgVelocity = Vector3.zero;
		Vector3 avgPosition = Vector3.zero;
		float count = 0;
		float f = 0.0f;
		float dirVector = 0.0f;
		Vector3 myPosition = transformComponent.position;
		Vector3 forceV;
		Vector3 toAvg;
		Vector3 wantedVel;

		for (int i = 0;i<objects.Length;i++)
		{
			Transform transform= objects[i];
			if (transform != transformComponent)
			{
				Vector3 otherPosition = transform.position;
				// Average position to calculate cohesion
				avgPosition += otherPosition;
				count++;
				//Directional vector from other flock to this flock
				forceV = myPosition - otherPosition;
				//Magnitude of that directional vector(Length)
				dirVector= forceV.magnitude;
				//Add push value if the magnitude, the length of the 
				//vector, is less than followRadius to the leader
				if (dirVector < followRadius)
				{
					//calculate the velocity, the speed of the object, based
					//on the avoidance distance between flocks if the 
					//current magnitude is less than the specified 
					//avoidance radius
					if (dirVector < avoidanceRadius)
					{
						f = 1.0f - (dirVector / avoidanceRadius);
						if (dirVector > 0) avgVelocity += 
						(forceV / dirVector) * f * avoidanceForce;
					}
					//just keep the current distance with the leader
					f = dirVector / followRadius;
					ZebraFlock tempOtherFlock = otherFlocks[i];
					//we normalize the tempOtherFlock velocity vector to get 
					//the direction of movement, then we set a new velocity
					avgVelocity += tempOtherFlock.normalizedVelocity * f * 
					followVelocity;  
				}
			}
		}

		if (count > 0)
		{
			//Calculate the average flock velocity(Alignment)
			avgVelocity /= count;
			//Calculate Center value of the flock(Cohesion)
			toAvg = (avgPosition / count) - myPosition;
		}  
		else 
		{
			toAvg = Vector3.zero;
		}
		//Directional Vector to the leader
		forceV = origin.position -  myPosition;
		dirVector = forceV.magnitude;   
		f = dirVector / toOriginRange;
		//Calculate the velocity of the flock to the leader
		if (dirVector > 0) //if this boid is not at the center of the flock
		{
			originPush = (forceV / dirVector) * f ;
		}

		if (speed < minSpeed && speed > 0) 
		{
			velocity = (velocity / speed) * minSpeed;
			//Debug.Log("speed "+speed);
		}

		wantedVel = velocity;
		//Calculate final velocity
		wantedVel -= wantedVel *  Time.deltaTime;
		wantedVel += randomPush * Time.deltaTime;
		wantedVel += originPush * Time.deltaTime;
		wantedVel += avgVelocity * Time.deltaTime;
		wantedVel += toAvg.normalized * gravity * Time.deltaTime;
		wantedVel.y = 0;
		//Final Velocity to rotate the flock into
		velocity = Vector3.RotateTowards(velocity, wantedVel, 
		turnSpeed * Time.deltaTime, 100.00f);
		transformComponent.rotation =  
		Quaternion.LookRotation(velocity);
		//Move the flock based on the calculated velocity
		transformComponent.Translate(velocity * Time.deltaTime, 
		Space.World);
		//normalise the velocity
		normalizedVelocity = velocity.normalized;
	}

	IEnumerator UpdateRandom() 
	{
		while (true) 
		{
			randomPush = Random.insideUnitSphere * randomForce;
			yield return new WaitForSeconds(randomFreq + 
			Random.Range(-randomFreq / 2.0f, randomFreq / 2.0f));
		}
	}

	// void EndPoint()
	// {
	//   Vector3 EndPoint = GameObject.FindGameObjectWithTag("Finish").transform.position;
	//   direction = EndPoint - transform.position;
	//   direction.y =0;
	//   float distance = direction.magnitude;
	//   if( distance <= 15)
	//   { 

	//   }
	// }
}
