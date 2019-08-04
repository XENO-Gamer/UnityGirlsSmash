using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UIを使用可能にする

public class FadeScript : MonoBehaviour {

    private GameObject MainCol;

    public float speed = 0.10f;  //透明化の速さ
    public bool FadeFlag = false;
    public bool CameraChange = false;
    float alfa;    //A値を操作するための変数
    float StopTaime = 0;
    float red, green, blue;    //RGBを操作するための変数


    // Use this for initialization
    void Start () {
        //Panelの色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        MainCol = GameObject.Find("GameMainControl");
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);

        if (FadeFlag == true) {
            if (alfa <= 1)
            {
                alfa += speed;
            }
            else
            {
                if (StopTaime <= 1){
                    StopTaime += speed;
                }
                else{
                    FadeFlag = false;
                    StopTaime = 0;
                    CameraChange = true;
                }
            }
        }
        else {
            if (alfa >= 0)
            {
                alfa -= speed;
            }
        }

    }
}
