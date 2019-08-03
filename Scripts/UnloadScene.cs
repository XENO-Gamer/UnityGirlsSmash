using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (SceneManager.GetSceneByName("Game").isLoaded)
        {
            SceneManager.UnloadScene("Game");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        }

    }

    public void Unload()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }

}
