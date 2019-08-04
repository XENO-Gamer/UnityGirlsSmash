using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Image>().fillAmount = 0;

    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Image>().fillAmount = this.gameObject.GetComponent<Image>().fillAmount;
    }
     
    public void HPDown(float damage)
    {
        this.gameObject.GetComponent<Image>().fillAmount = damage / 100;
        Debug.Log(damage);
    }
}
