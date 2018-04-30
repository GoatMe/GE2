using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

	public static SceneControl control;

	//theatrical black curtain before/after scene
	public int index;
	public Image blackImage;
	public Animator anim;
	private bool isCoroutineStarted = false;

	// Create a temporary reference to the current scene.
	Scene currentScene;

	// Retrieve the name of this scene.
	string sceneName;

	//To trigger the scene change
	private GameObject a;
	private ZebraMainFlockController fc;

	private GameObject b;
	private TigerPursue tp;

	private GameObject c;
	private BirdsPathFollow bpf;

	//list of all zebras
	public List<Vector3> allPos = new List<Vector3>();
	
	private float distance1 = 0f;
	private float distance2 = 0f;

	void Awake()
	{
		//Check if there is a ScenContoller present
		//if not, then do not destroy the controller.
		//Else if this is controller is not of this object, destroy it;
		if(control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy(gameObject);
		}
		
	}

	// Use this for initialization
	void Start () 
	{
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

		isCoroutineStarted = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (sceneName == "MainScene") 
        {
        	isCoroutineStarted = false;
            //check if the GameObject is loaded.
			if (a == null)
			{
				a = GameObject.FindWithTag("LeaderZebra");
				fc = a.GetComponent<ZebraMainFlockController>();
			}
			//check distance between the gO and the endpoint 
			distance1 = fc.distance;
			// Debug.Log("distance "+distance);
			
			//trigger to initiate scene change
			if(distance1 <= 3 && distance1 >= 1)
			{	
				if(!isCoroutineStarted)
		    	{
		    		Debug.Log("Next Scene Coroutine Started");
					StartCoroutine(Fading());
				}
			}
        }
        else if (sceneName == "Hunt")
        {
	        if (b == null)
			{
				b = GameObject.FindWithTag("Tiget");
				tp = b.GetComponent<TigerPursue>();
			}
			//check distance between the gO and the target 
			distance2 = tp.direction.magnitude;
			// Debug.Log("distance "+tp.direction);
			
			//trigger to initiate scene change
			if(distance2 <= 10)
			{	
				if(!isCoroutineStarted)
		    	{
		    		Debug.Log("Next Scene Coroutine Started");
					//	StartCoroutine(Fading());
				}
			}
        }
        else if (sceneName == "BirdView")
        {
        	isCoroutineStarted = false;
            //check if birds have reacher the last checkpoint then 
			if (c == null)
			{
				c = GameObject.FindWithTag("Birds");
				bpf = c.GetComponent<BirdsPathFollow>();
			}
			//check distance between the gO and the target 
			int i = bpf.index;
			// Debug.Log("distance "+distance);
			
			//trigger to initiate scene change
			if(i == 3)
			{	
				if(!isCoroutineStarted)
		    	{
		    		Debug.Log("Next Scene Coroutine Started");
					StartCoroutine(Fading());
				}
			}
		}
	}

	IEnumerator Fading()
	{
		//to stop activating multiple times
		isCoroutineStarted = true;

		if (sceneName == "Hunt")
		{
			yield return new WaitForSeconds(30);
		}

		//activate the theatrical black curtain 
		anim.SetBool("Fade", true);
		yield return new WaitUntil(()=>blackImage.color.a==1);
		//take in current data of the objects in the list
		List<GameObject> allPrefabsPos = new List<GameObject>(ZebraCreator.allZebra);
				//load the saved position into another scene
				for (int i = 0; i < allPrefabsPos.Count; i++)
				{
					//Debug.Log(allPrefabsPos[i].transform.position);
					allPos.Add(allPrefabsPos[i].transform.position);
				}
		//change scene
		SceneManager.LoadScene(index);
	}
}
