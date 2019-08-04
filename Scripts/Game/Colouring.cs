using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Colouring : MonoBehaviour {

    float level;
    private GameObject _parent2;            //親オブジェクト名
    Color color;

    // Use this for initialization
    void Start () {
        //親オブジェクトを取得
        _parent2 = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        level = _parent2.gameObject.GetComponent<Player1Control>().level;

        color = new Color(1f, 1f, 1f, level);

        GetComponent<Renderer>().material.color = color;
    }
}
