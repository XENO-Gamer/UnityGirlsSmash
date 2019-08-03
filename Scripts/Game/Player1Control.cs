using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Control : MonoBehaviour {
    public static float speed = 3.0f;       //加速度
    public const int MaxJumpCount = 2;      //ジャンプ最大回数

    [SerializeField]
    private string horizontalString;
    [SerializeField]
    private string verticalString;
    [SerializeField]
    private string fire1String;
    [SerializeField]
    private string JanpString;
    [SerializeField]
    private string AttackString;

    public int ControlFlag = 0;
    int UpFlag = 0;
    int DownFlag = 0;
    int RighrFlag = 0;
    int LeftFlag = 0;
    bool JanpFlag = false;
    bool GroundFlag = true;     //接地しているか
    bool AttackFlag = true;
    public float JumpPpower;        //ジャンプの高さ
    private Rigidbody rd;        //ジャンプ計算
    private Animator animator;
    private GameObject _parent; //親オブジェクト名


    GameObject Unitychan;
    UnityChanScript unitychan_script;

    // Use this for initialization
    void Start() {

        rd = GetComponent<Rigidbody>();

        Unitychan = GameObject.Find("UTC_Default"); 
        unitychan_script = Unitychan.GetComponent<UnityChanScript>();

        //親オブジェクトを取得
        _parent = transform.root.gameObject;

        animator = GetComponentInChildren<Animator>();

        Debug.Log("Parent:" + _parent.name);

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis(verticalString) == 1) UpFlag = 1;
        else UpFlag = 0;
        if (Input.GetAxis(verticalString) == -1) DownFlag = 10;
        else DownFlag = 0;
        if (Input.GetAxis(horizontalString) == 1) RighrFlag = 100;
        else RighrFlag = 0;
        if (Input.GetAxis(horizontalString) == -1) LeftFlag = 1000;
        else LeftFlag = 0;
        if (Input.GetButtonDown(JanpString)) JanpFlag = true;
        else JanpFlag = false;

        if (Input.GetButtonDown(AttackString)) AttackFlag = true;
        else AttackFlag = false;

       // Debug.Log(AttackFlag);

        ControlFlag = UpFlag + DownFlag + RighrFlag + LeftFlag;


        Work(ControlFlag);  //歩き

        JanpAction(JanpFlag); //ジャンプ

        AttackAction(AttackFlag); //攻撃

    }

    //--------歩き------------

    public void Work(int ControlFlag)
    {

        if (ControlFlag != 0){
            move(); //進む
            animator.SetInteger("ControlFlag", 1);
        }
        else animator.SetInteger("ControlFlag", 0);
        switch (ControlFlag)
        {
            case 1:direction(0); break;     //上向き

            case 10:direction(180); break;  //下向き

            case 100:direction(90); break;  //右向き

            case 1000:direction(270); break;//左向き

            case 101:direction(45); break;  //向き

            case 110:direction(135); break; //向き

            case 1001:direction(315); break; //向き

            case 1010:direction(225); break; //向き
        }
        return;
    }

    //--------進む------------
    public void move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        return;
    }

    //--------向き------------
    public void direction(int angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
        return;
    }

    //--------ジャンプ------------
    public void JanpAction(bool JanpFlag)
    {

        if (JanpFlag && GroundFlag)
        {
            animator.SetBool("JampFlag", true);
            rd.velocity = Vector3.up * JumpPpower;//ジャンプ計算
            GroundFlag = false;//足が地に着いてないよー

        }
        else animator.SetBool("JampFlag", false);

    }

    //--------攻撃------------
    public void AttackAction(bool AttackFlag)
    {
        if (AttackFlag)
        {
            animator.SetBool("Attack", true);

        }
        else animator.SetBool("Attack", false);

    }


    // 衝突した瞬間に呼ばれる 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))//足が地面に着いてるか？
        {
            GroundFlag = true;//足が地面に着いてるよー
        }
    }


}

