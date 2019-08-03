using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopController : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        MainControl.ActiveScene = (Scene)Enum.ToObject(typeof(Scene), 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
