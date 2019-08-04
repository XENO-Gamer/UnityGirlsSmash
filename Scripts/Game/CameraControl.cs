using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private GameObject mainCame;
    private GameObject P1Came;
    private GameObject P2Came;
    private GameObject maincol;
    private GameObject PausePanel;

    int cnt;


    public bool mainCame_Flag;
    public bool P1Came_Flag;
    public bool P2Came_Flag;

    // Use this for initialization
    void Start () {
        mainCame = transform.Find("Game_Main_Camera").gameObject;
        P1Came = transform.Find("Camera_Player1").gameObject;
        P2Came = transform.Find("Camera_Player2").gameObject;
        maincol = GameObject.Find("GameMainControl");
        PausePanel = GameObject.Find("PausePanel");
        PausePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        mainCame.SetActive(maincol.GetComponent<MainCol>().mainCame_Flag);  //メインカメラフラグがTrueの時にメインカメラに切り替え
        P1Came.SetActive(maincol.GetComponent<MainCol>().P1Came_Flag);      //Player1カメラフラグがTrueの時にPlayer1カメラに切り替え
        P2Came.SetActive(maincol.GetComponent<MainCol>().P2Came_Flag);      //Player2カメラフラグがTrueの時にPlayer1カメラに切り替え

        cnt++;

        if(maincol.GetComponent<MainCol>().P1Came_Flag == true || maincol.GetComponent<MainCol>().P2Came_Flag == true)
        {
            Invoke("pauseMethod",4.5f); //カメラフラグがtrueになってから数秒後にリザルト画面を表示
        }

        Debug.Log(maincol.GetComponent<MainCol>().P1Came_Flag + " " + maincol.GetComponent<MainCol>().P2Came_Flag);

    }

    //リザルトメソッド
    void pauseMethod(){
        PausePanel.SetActive(true);
    }
}
