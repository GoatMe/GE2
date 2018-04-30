using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TigerScriptControler : MonoBehaviour {

public TigerPathFollow follow;
public TigerPursue pursue;

// Create a temporary reference to the current scene.
	Scene currentScene;

	// Retrieve the name of this scene.
	string sceneName;
  
  void Start () 
  {
	currentScene = SceneManager.GetActiveScene ();
	sceneName = currentScene.name;

	follow  = GetComponent<TigerPathFollow> ();
	pursue = GetComponent<TigerPursue> ();
      
	follow.enabled = true;
	pursue.enabled = false;
  }

	void Update () 
	{
  // 		for (int i = 0; i < pursue.allPrefabsPos.Count; i++)
		// {
			if(sceneName == "Hunt")
			{
				Debug.Log("hunt");
				Debug.Log("hunt"+pursue.direction.magnitude);
				if (pursue.direction.magnitude <= 30)
				{
					Debug.Log("Pursue");
	 				follow.enabled = false;
	        		pursue.enabled = true;
			    }
			    
				
			}
			else if (pursue.direction.magnitude >= 30)
			    {
			    	follow.enabled = true;
			    	pursue.enabled = false;
			    }
		// }
	}
}