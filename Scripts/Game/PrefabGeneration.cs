using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGeneration : MonoBehaviour {

    public int CharaID;                              //キャラクターID

    //private int CharaID = MainControl.Player1;     //キャラクターID

    [SerializeField]
    public int Stock;        //PlayerのStock

    public float Position_X, Position_Y, Position_Z;  //指定位置
    public float Rotation_X, Rotation_Y, Rotation_Z;  //指定向き

    public string PrefabPASS;      //プレハブの指定パス
    public string PrefabName;      //プレハブ名

    public float Jump_Ppower;                //ジャンプの高さ
    public float Attack_Power;               //攻撃力
    public float speed;                     //スピード

    [SerializeField]
    public string horizontalString;
    [SerializeField]
    public string verticalString;
    [SerializeField]
    public string fire1String;
    [SerializeField]
    public string JumpString;
    [SerializeField]
    public string AttackString;

    [SerializeField]
    public int Player;                       //自プレイヤー
    [SerializeField]
    public string VsPlayer;                     //相手プレイヤー
    [SerializeField]
    public int VsCharaID;                       //相手のキャラID
    [SerializeField]
    public string VsCharaName;                  //相手のキャラ名
    [SerializeField]
    public float VsAttack_Power;                //攻撃力

    GameObject prefab;
    GameObject obj;
    GameObject VSObjName;   //相手のオブジェクト
    GameObject StageObj;
    PrefabGeneration VsScript;

    Prefab_Stage prefab_Stage;
    int StageID;

    // Use this for initialization
    void Start()
    {
        switch (Player) {
            case 1:
                CharaID = MainControl.Player1 + 1;
                VsCharaID = MainControl.Player2 + 1;

            break;

            case 2:
                CharaID = MainControl.Player2 + 1;
                VsCharaID = MainControl.Player1 + 1;
           break;
        }


        StageObj = GameObject.Find("StageControl");                 //「StageControl」オブジェクトを指定
        StageID = StageObj.GetComponent<Prefab_Stage>().StageID;    //「StageControl」オブジェクト内からステージIDを取得
       // Debug.Log("ステージID(1)＝" + StageID);
        Respawn(Player);                                            //リスポーン発生位置


        PrefabName = CharaIDSelect(CharaID); //キャラクターID毎のキャラ名を取得

        CharaStatus(CharaID); //キャラクター毎のステータスを取得

        PrefabPASS = "Prefabs/" + PrefabName;

        // プレハブを取得
        prefab = (GameObject)Resources.Load(PrefabPASS);

        // プレハブからインスタンスを生成
        obj = (GameObject)Instantiate(prefab, new Vector3(Position_X, Position_Y, Position_Z), Quaternion.identity);

        // 作成したオブジェクトを子として登録
        obj.transform.parent = transform;

        // 作成した子オブジェクトに名前を設定
        obj.name = PrefabName;  //この処理が無いと、子オブジェクト名の後に「(Clone)」が付いてしまう


        VSObjName = GameObject.Find(VsPlayer);//相手のオブジェクト情報取得

        VsScript = VSObjName.GetComponent<PrefabGeneration>();

        //VsCharaID = VsScript.CharaID;

        VsCharaName = CharaIDSelect(VsCharaID); //相手のキャラクターID毎の相手のキャラ名を取得

        Stock = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Stock = obj.GetComponent<Player1Control>().Stock;

    }


    public string CharaIDSelect(int CharaID)
    {
        switch (CharaID)
        {
            case 1:
                PrefabName = "UnityChan";
                break;

            case 2:
                PrefabName = "Misaki_SchoolUniform_summer";
                break;

            case 3:
                PrefabName = "Yuko_SchoolUniform_summer";
                break;

            case 4:
                PrefabName = "UTC_SchoolUniform_summer";
                break;

            case 5:
                PrefabName = "UTC_SchoolUniform_Winter";
                break;

            case 6:
                PrefabName = "Misaki_SchoolUniform_Winter";
                break;

            case 7:
                PrefabName = "Yuko_SchoolUniform_Winter";
                break;
            default:
                PrefabName = "なし";
                break;

        }

        return PrefabName;
    }

    void CharaStatus(int CharaID)
    {
        switch (CharaID)
        {
            case 1:
            case 4:
            case 5:
                Attack_Power = 10;
                Jump_Ppower = 5;
                speed = 3.0f;
                break;

            case 2:
            case 6:
                Attack_Power = 20;
                Jump_Ppower = 5;
                speed = 2.0f;
                break;

            case 3:
            case 7:
                Attack_Power = 5;
                Jump_Ppower = 8;
                speed = 4.0f;
                break;

            default:
                Attack_Power = 0;
                break;

        }
    }

    //リスポーン発生位置
    public float Respawn(int Player)
    {
        switch (StageID)   //ステージ毎で復活発生位置変更
        {
            case 1:
                switch (Player)
                {
                    case 1:
                        Position_X = -2; Position_Y = -1.0f; Position_Z = 0;

                        break;

                    case 2:
                        Position_X = 2; Position_Y = -1.0f; Position_Z = 0;

                        break;
                }
                break;

            case 2:
                switch (Player)
                {
                    case 1:
                        Position_X = -2.5f; Position_Y = -4.0f; Position_Z = 0;

                        break;

                    case 2:
                        Position_X = 2.5f; Position_Y = -4.0f; Position_Z = 0;

                        break;
                }
                break;

            case 3:
                switch (Player)
                {
                    case 1:
                        Position_X = -8.0f; Position_Y = -3.3f; Position_Z = 3.0f;

                        break;

                    case 2:
                        Position_X = 8.0f; Position_Y = -3.3f; Position_Z = 3.0f;

                        break;
                }
                break;

            default:
                switch (Player)
                {
                    case 1:
                        Position_X = -2; Position_Y = 0; Position_Z = 0;

                        break;

                    case 2:
                        Position_X = 2; Position_Y = 0; Position_Z = 0;

                        break;
                }
                break;

        }//switch (StageID) end

        return 0;
    }//public float Respawn()end

}
