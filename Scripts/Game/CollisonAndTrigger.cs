using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonAndTrigger : MonoBehaviour {

    public Player1Control player1control;

    private Animator animator;
    public bool GroundFlag= true;     //接地しているか

    void Start()
    {

        //GroundFlag = player1control.GroundFlag;

    }

        // 衝突した瞬間に呼ばれる 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))//足が地面に着いてるか？
        {
            GroundFlag = true;//足が地面に着いてるよー
            Debug.Log(GroundFlag);
        }
    }

    // トリガーイベントに侵入した瞬間に呼ばれる 
    private void OnTriggerEnter(Collider collision)
    {
        //相手の攻撃判定
        if (collision.gameObject.tag == "Attack")
        {
            Debug.Log("痛っ");//攻撃受けたら
            animator.SetTrigger("Down"); //ダメージモーション
        }
    }
}
