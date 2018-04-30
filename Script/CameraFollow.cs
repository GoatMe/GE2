using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {

	public GameObject target;

	public float speed = 5.0f;
public float distance = 0f;
	public float smoothSpeed = 10f;
	public Vector3 offset;
	public float rotationSpeed = 1.0f;

// Create a temporary reference to the current scene.
	Scene currentScene;

	// Retrieve the name of this scene.
	string sceneName;

	void Start()
	{
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;
		
				

	}
	void FixedUpdate()
	{
		if (sceneName == "MainScene") 
        {

			if (target == null)
			{
				target = GameObject.FindWithTag("LeaderZebra");
				offset = transform.position - target.transform.position;
			}
        }
        else if (sceneName == "Hunt")
        {
	        if (target == null)
			{
				target = GameObject.FindWithTag("Tiget");
				offset = transform.position - target.transform.position;
			}
        }
        else if (sceneName == "BirdView")
        {
			if (target == null)
			{
				target = GameObject.FindWithTag("Birds");
				offset = transform.position - target.transform.position;
			}

		}

		PathFollow ();
	}

	public void PathFollow()
	{
		Debug.Log(target.transform.position);
		Vector3 direction = target.transform.position - transform.position;

		float distance = direction.magnitude; 
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		Vector3 moveVector = direction.normalized * Time.deltaTime * speed;
		transform.position += moveVector;
	}
}
