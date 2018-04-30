using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraAnimation : MonoBehaviour {
	private float distance = 0f;

	public GameObject a;
	public ZebraFlock fc;
	// = GetComponent<UnityFlockController>;

	Animator anim;
	int idle = Animator.StringToHash("Idle");
	int graze = Animator.StringToHash("Graze");
	int running = Animator.StringToHash("Run");
	int walking = Animator.StringToHash("Walk");

	 int runStateHash = Animator.StringToHash("Base Layer.Idle");

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		a = GameObject.FindGameObjectWithTag("LeaderZebra");
		fc = GetComponent<ZebraFlock>();
	}
	
	// Update is called once per frame
	void Update () {
		distance = fc.distance;
		//Debug.Log("distance "+distance);
			if (distance <= fc.ranDistStop)
		{
			anim.SetTrigger (graze);	
			//anim.SetFloat ("Speed", 0.6f);
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
