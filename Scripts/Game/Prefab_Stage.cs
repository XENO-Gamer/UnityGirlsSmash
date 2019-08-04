using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Stage : MonoBehaviour {

    public int StageID;


    public float Position_X, Position_Y, Position_Z;  //指定位置
    public float Rotation_X, Rotation_Y, Rotation_Z;  //指定向き

    public float Player1Respawn_X, Player1Respawn_Y, Player1Respawn_Z;  //Player1 リスポーン場所
    public float Player2Respawn_X, Player2Respawn_Y, Player2Respawn_Z;  //Player2 リスポーン場所


    public string Stage_PrefabPASS;      //ステージのプレハブの指定パス
    public string Stage_PrefabName;      //ステージのプレハブ名

    // Skyboxのマテリアル
    public Material skybox, skyboxNo1, skyboxNo2, skyboxNo3;



    // Use this for initialization
    void Start () {
        Stage_PrefabName = StageIDSelect(MainControl.StageSelect + 1/*StageID*/); //ステージID毎のキャラ名を取得
        StageID = MainControl.StageSelect + 1;
       // StageID = 3;
        Debug.Log("ステージID : " + StageID);

        Stage_PrefabName = StageIDSelect(StageID); //ステージID毎のステージ名を取得
        Stage_PrefabPASS = "Prefabs/" + Stage_PrefabName;

        skybox = SkyboxSelect(StageID);

        // Skyboxを変更する
        RenderSettings.skybox = skybox;

        // プレハブを取得
        GameObject prefab = (GameObject)Resources.Load(Stage_PrefabPASS);

        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(prefab, new Vector3(Position_X, Position_Y, Position_Z), Quaternion.identity);


        // 作成したオブジェクトを子として登録
        obj.transform.parent = transform;

        // 作成した子オブジェクトに名前を設定
        obj.name = Stage_PrefabName;  //この処理が無いと、子オブジェクト名の後に「(Clone)」が付いてしまう


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string StageIDSelect(int StageID)
    {
        switch (StageID)
        {
            case 1:
                Stage_PrefabName = "Stage1";
                break;

            case 2:
                Stage_PrefabName = "chair";
                break;

            case 3:
                Stage_PrefabName = "ladder";
                break;

            default:
                Stage_PrefabName = "なし";
                break;

        }

        return Stage_PrefabName;
    }

    public Material SkyboxSelect(int StageID)
    {
        switch (StageID)
        {
            case 1:
                return skyboxNo1;

            case 2:
                return skyboxNo2;

            case 3:
                return skyboxNo3;

            default:
                return skyboxNo1;

        }

    }

    //取得用関数
    public int StegeID_A()
    {
        return StageID;
    }

}

