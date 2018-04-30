using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAbnormalities : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 0.2f;
		pos.y -= 160.3f; // because terrain had been offset on Y-axis by -160.3
		if(transform.position.y > pos.y)
		{
			transform.position = pos;
		}
		if(transform.rotation.z < 0.0f || transform.rotation.z < 10.0f)
		{
			transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,0.0f,transform.rotation.w);
		}
	}
}
