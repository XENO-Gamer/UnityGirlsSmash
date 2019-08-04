using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player1Control : MonoBehaviour
{
    public float Position_X, Position_Y, Position_Z;  //指定位置
    public float Rotation_X, Rotation_Y, Rotation_Z;  //指定向き
    public float speed;       //加速度
    public const int MaxJumpCount = 2;      //ジャンプ最大回数

    public string horizontalString;         //上下ボタン
    public string verticalString;           //左右ボタン
    public string fire1String;              //
    public string JumpString;               //ジャンプボタン
    public string AttackString;             //攻撃ボタン
    public string VsPlayer;                 //相手のPlayer名
    public string VsCharaName;              //相手が使用しているキャラクタ
    public int VsCharaID;


    public int Stock;        //PlayerのStock

    Vector3 VsRotation;                     //相手のVsRotation情報を取得

    public AudioClip sound1;//攻撃
    public AudioClip sound2;//勝ちセリフ
    public AudioClip sound3;//ダメージ
    public AudioClip sound4;//落下
    public AudioClip sound5;//ジャンプ

    public AudioClip[] sound;//サウンド

    public int ControlFlag = 0;
    int UpFlag = 0;
    int DownFlag = 0;
    int RighrFlag = 0;
    int LeftFlag = 0;

    public int Player;                       //自プレイヤー
    public float Jump_Ppower;                //ジャンプ力
    public float Attack_Power;              //攻撃力
    public float DamegeMeter;               //ダメージ量(多ければ多いほど吹っ飛びやすくなる)
    public float Momentum_Power = 0;        //飛ばされた時の威力
    public float VsAttack_Power;            //相手の攻撃力
    public float InvinicibleTime = 3.0f;    //無敵時間
    public float level;                     //無敵状態にモデルの色を変えるための変数

    float time = 0;                         //時間を記録する小数も入る変数
    float RevivalTime = 5;                  //復活までの時間
    float InvinicibleTimeCount;             //無敵時間カウント用

    bool JumpFlag = false;
    bool GroundFlag = false;                 //接地しているか
    bool AttackFlag = false;
    bool DamageFlag = false;
    bool NoMoveFlag = false;                //動かいないかどうか
    bool DropFlag = false;                  //落ちたかどうか
    bool Stopper = false;                   //連続Sound防止
    bool InvinicibleFlag = false;           //無敵フラグ
    bool JanpStopperFlag = false;           //落下した時のジャンプフラグ


    private Rigidbody rd;                   //ジャンプ計算
    private Animator animator;
    private GameObject _parent;             //親オブジェクト名

    public Collider col;                    //　拳のコライダ

    GameObject VSObjName;   //相手のオブジェクト
    PrefabGeneration VsScript;

    AudioSource audioSource;

    private GameObject gauge1;              //ダメージゲージ1
    private GameObject gauge2;              //ダメージゲージ2
    private HpSystem hpSystem1;             //ダメージゲージ1のフィールド
    private HpSystem hpSystem2;             //ダメージゲージ1のフィールド

    private GameObject maincol;
    private GameObject GamePad;

    Renderer _renderer;

    // Use this for initialization
    void Start()
    {

        rd = GetComponent<Rigidbody>();

        col.enabled = false; //攻撃当たり判定OFF

        //親オブジェクトを取得
        _parent = transform.root.gameObject;

        animator = GetComponentInChildren<Animator>();

        // 親オブジェクトからキー名を取得
        horizontalString = _parent.gameObject.GetComponent<PrefabGeneration>().horizontalString;
        verticalString = _parent.gameObject.GetComponent<PrefabGeneration>().verticalString;
        fire1String = _parent.gameObject.GetComponent<PrefabGeneration>().fire1String;
        JumpString = _parent.gameObject.GetComponent<PrefabGeneration>().JumpString;
        AttackString = _parent.gameObject.GetComponent<PrefabGeneration>().AttackString;

        //親オブジェクトからrotation情報を取得
        Position_X = _parent.gameObject.GetComponent<PrefabGeneration>().Position_X;
        Position_Y = _parent.gameObject.GetComponent<PrefabGeneration>().Position_Y;
        Position_Z = _parent.gameObject.GetComponent<PrefabGeneration>().Position_Z;

        //親オブジェクトからrotation情報を取得
        Rotation_X = _parent.gameObject.GetComponent<PrefabGeneration>().Rotation_X;
        Rotation_Y = _parent.gameObject.GetComponent<PrefabGeneration>().Rotation_Y;
        Rotation_Z = _parent.gameObject.GetComponent<PrefabGeneration>().Rotation_Z;
        transform.rotation = Quaternion.Euler(Rotation_X, Rotation_Y, Rotation_Z);

        // 親オブジェクトから相手の情報を取得
        VsPlayer = _parent.gameObject.GetComponent<PrefabGeneration>().VsPlayer;                    //相手のPlayer名
        VsCharaName = _parent.gameObject.GetComponent<PrefabGeneration>().VsCharaName;              //相手のCharacter名
        VSObjName = GameObject.Find(VsPlayer);//相手のオブジェクト情報取得
        VsScript = VSObjName.GetComponent<PrefabGeneration>();
        VsAttack_Power = VsScript.Attack_Power;   //相手の攻撃力

        Player = _parent.gameObject.GetComponent<PrefabGeneration>().Player;


        //親オブジェクトかキャラクターのステータス情報を取得
        Attack_Power = _parent.gameObject.GetComponent<PrefabGeneration>().Attack_Power;
        Jump_Ppower = _parent.gameObject.GetComponent<PrefabGeneration>().Jump_Ppower;
        speed = _parent.gameObject.GetComponent<PrefabGeneration>().speed;
        Stock = _parent.gameObject.GetComponent<PrefabGeneration>().Stock;

        //20190125
        var VSTransform = GameObject.Find("/" + VsPlayer + "/" + VsCharaName).transform;              //相手のtransform情報を取得
        VsRotation = VSTransform.eulerAngles;

        audioSource = GetComponent<AudioSource>();

        DamegeMeter = 0;                        //ダメージメータ初期化
        InvinicibleTimeCount = InvinicibleTime; //無敵時間初期化

        //ダメージゲージの定義
        gauge1 = GameObject.Find("hpGauge1-2");
        gauge2 = GameObject.Find("hpGauge2-2");
        hpSystem1 = gauge1.GetComponent<HpSystem>();
        hpSystem2 = gauge2.GetComponent<HpSystem>();

        MainControl.StockLF1 = Stock;
        MainControl.StockLF2 = Stock;

        maincol = GameObject.Find("GameMainControl");
        GamePad = GameObject.Find("P" + Player);

        level = 1.0f;   
    }

    // Update is called once per frame
    void Update()
    {

        //一時停止中のUpdate関数の制御（以下の処理を無効化）
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // 入力を取得
        var v1 = Input.GetAxis(verticalString);
        var h1 = Input.GetAxis(horizontalString);

        if (maincol.GetComponent<MainCol>().GameStartFlag && !maincol.GetComponent<MainCol>().GameEndFlag)
        {
            // Aボタンを押したらジャンプフラグが発動
            if (GamePad.GetComponent<Gamepad>().A_Button == true)
            {
                JumpFlag = true;
                GamePad.GetComponent<Gamepad>().A_Button = false;
            }
            else JumpFlag = false;

            // Bボタンを押したら攻撃フラグが発動
            if (GamePad.GetComponent<Gamepad>().B_Button == true)
            {
                AttackFlag = true;
                GamePad.GetComponent<Gamepad>().B_Button = false;
            }
            else AttackFlag = false;

            ControlFlag = UpFlag + DownFlag + RighrFlag + LeftFlag;

            time -= Time.deltaTime;//毎フレームの時間を加算.

            if (time <= 1.8f)
            {
                Momentum_Power = 0;
            }

            if (time <= 0)
            {
                DamageFlag = false;
                time = 0;
            }

            transform.position -= transform.forward * (DamegeMeter / 5000) * Momentum_Power;//ダメージ受けた時に吹き飛ばされる

            if (DamageFlag == false)    //  ダメージを受けている時は動けない
            {

                // スティックが倒れていれば、倒れている方向を向く
                if ((h1 != 0 || v1 != 0))
                {
                    move();      //進む
                    angle(h1, v1);//向き
                    animator.SetInteger("ControlFlag", 1); //走りモーション
                }
                else animator.SetInteger("ControlFlag", 0);//走りモーション解除

                JumpAction(JumpFlag); //ジャンプ

                AttackAction(AttackFlag); //攻撃
            }

            if (DropFlag == true)
            {
                Revival();//復活処理
            }
        }
        else
        {
            GamePad.GetComponent<Gamepad>().A_Button = false;
            GamePad.GetComponent<Gamepad>().B_Button = false;

        }

        var VSTransform = GameObject.Find("/" + VsPlayer + "/" + VsCharaName).transform;  //相手のtransform情報を取得
        VsRotation = VSTransform.eulerAngles;

        //ゲーム終了時キャラ処理
        if(maincol.GetComponent<MainCol>().GameEndFlag == true)
        {
            GameEnd();
        }

        //無敵出来る時間計測
        if(InvinicibleFlag)
        {
            level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            InvinicibleTimeCount -= Time.deltaTime;
            if (InvinicibleTimeCount <= 0)
            {
                InvinicibleFlag = false;
                level = 1.0f;
                InvinicibleTimeCount = InvinicibleTime;
            }
        }

    }
    //--------進む------------
    public void move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        return;
    }

    //--------向き------------
    public void angle(float HorizontalFlag, float VerticalFlag)
    {
        var direction = new Vector3(HorizontalFlag, 0, VerticalFlag);
        transform.localRotation = Quaternion.LookRotation(direction);
        return;
    }

    //--------ジャンプ------------
    public void JumpAction(bool JumpFlag)
    {

        if (JumpFlag && GroundFlag && !JanpStopperFlag)
        {
            animator.SetBool("JampFlag", true);
            audioSource.PlayOneShot(sound5);
            rd.velocity = Vector3.up * Jump_Ppower;//ジャンプ計算
            GroundFlag = false;//足が地に着いてないよー

        }
        else animator.SetBool("JampFlag", false);

    }

    //--------攻撃------------
    public void AttackAction(bool AttackFlag)
    {
        if (AttackFlag && GroundFlag && !JanpStopperFlag)
        {
            animator.SetTrigger("Attack");
            audioSource.PlayOneShot(sound1);
        }

    }

    //--------復活処理---------------------
    public void Revival()
    {

        RevivalTime -= Time.deltaTime;               　//毎フレームの時間を減算

        if (RevivalTime <= 0)
        {

            if(VsPlayer == "Player1")
            {
                MainControl.StockLF2 = Stock;
            }
            else
            {
                MainControl.StockLF1 = Stock;
            }

            DamegeMeter = 0; //落下したらダメージ初期化
            RevivalTime = 5; //時間初期化
            DropFlag = false;
            JanpStopperFlag = false;
            InvinicibleFlag = true;
            transform.position = new Vector3(Position_X, Position_Y, Position_Z);
            transform.rotation = Quaternion.Euler(Rotation_X, Rotation_Y, Rotation_Z);

            //ゲージの処理
            if (VsPlayer == "Player1")
            {
                hpSystem2.HPDown(DamegeMeter);
            }
            else
            {
                hpSystem1.HPDown(DamegeMeter);
            }
        }

    }

    //--------ゲーム終了処理---------------------
    public void GameEnd()
    {
        if (Stopper == false && VsScript.Stock <= 0)
        {
            audioSource.PlayOneShot(sound2);
            Stopper = true;
        }
        transform.position = new Vector3(Position_X, Position_Y, Position_Z);
        transform.rotation = Quaternion.Euler(Rotation_X, Rotation_Y, Rotation_Z);

    }

    // 衝突した瞬間に呼ばれる 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))//足が地面に着いてるか？
        {
            GroundFlag = true;//足が地面に着いてる
        }
        else GroundFlag = false;//足が地面から離れている
    }

    // トリガーイベントに侵入した瞬間に呼ばれる 
    private void OnTriggerEnter(Collider collision)
    {

        //ダメージを受けたら
        if (collision.gameObject.tag == "Attack" && InvinicibleFlag == false)
        {
            DamageFlag = true;
            time = 2;

            DamegeMeter += VsAttack_Power;
            Momentum_Power = 25;

            //ダメージの上限設定
            if (DamegeMeter >= 100)
            {
                DamegeMeter = 100;
            }

            audioSource.PlayOneShot(sound3);
            transform.rotation = Quaternion.Euler(0, VsRotation.y + 180, 0); //攻撃した相手の向きの対になる
            animator.SetTrigger("Down"); //ダメージモーション
            InvinicibleFlag = true; //無敵状態


            //ゲージの処理
            if (VsPlayer == "Player1")
            {
                hpSystem2.HPDown(DamegeMeter);
            }
            else
            {
                hpSystem1.HPDown(DamegeMeter);
            }
        }
       
        //落下したら
        if (collision.gameObject.tag == "Drop")
        {
            Stock = Stock - 1;
            if (Stock >= 1)
            {
                audioSource.PlayOneShot(sound4);
            }
            DropFlag = true; //落ちたよ！
        }

        //ジャンプできなくなるストッパー
        if (collision.gameObject.tag == "JanpStopper")
        {
            JanpStopperFlag = true;
        }


    }

    // トリガーイベントを触れるのをやめたとき
    private void OnTriggerExit(Collider collision)
    {

    }
    //　攻撃した時のイベント
    void AttackStart()
    {
        NoMoveFlag = true;
        col.enabled = true;//攻撃のトリガーを出現
    }

    //　攻撃やり終えた時のイベント
    void AttackEnd()
    {
        NoMoveFlag = false;
        col.enabled = false;//攻撃のトリガーを取り消し
        animator.SetTrigger("return");//待機モーション
    }

    void DounEventStart()
    {
        NoMoveFlag = true;
        //レイヤーをPlayerに変更
        gameObject.layer = LayerMask.NameToLayer("Invincible");
    }

    void DounEventEnd()
    {
        NoMoveFlag = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        animator.SetTrigger("return");//待機モーション
    }

}

