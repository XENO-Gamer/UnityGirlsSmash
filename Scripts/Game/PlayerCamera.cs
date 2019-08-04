using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    float Position_X, Position_Y, Position_Z;  //指定位置
    public string Player;                       //自プレイヤー

    GameObject StageObj;
    Prefab_Stage prefab_Stage;
    int StageID;


    // Use this for initialization
    void Start () {
        StageObj = GameObject.Find("StageControl");                 //「StageControl」オブジェクトを指定
        StageID = StageObj.GetComponent<Prefab_Stage>().StageID;    //「StageControl」オブジェクト内からステージIDを取得

        switch (StageID)
        {
            case 1:
                if (Player == "Player1") Position_X = -0.8f; Position_Y = 0.0f; Position_Z=0.0f;
                if (Player == "Player2") Position_X = 0.8f; Position_Y = 0.0f; Position_Z = 0.0f;
                break;
            case 2:
                if (Player == "Player1") Position_X = -1.3f; Position_Y = -3.3f; Position_Z = 0.0f;
                if (Player == "Player2") Position_X = 1.3f; Position_Y = -3.3f; Position_Z = 0.0f;
                break;
            case 3:
                if (Player == "Player1") Position_X = -6.6f; Position_Y = -2.55f; Position_Z = 3.0f;
                if (Player == "Player2") Position_X = 6.6f; Position_Y = -2.55f; Position_Z = 3.0f;
                break;
        }

        transform.position = new Vector3(Position_X, Position_Y, Position_Z);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
