using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanScript : MonoBehaviour {


    private Animator animator;


    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int WorkMotion(int WorkFlag)
    {
        animator.SetInteger("ControlFlag", WorkFlag);


        return 0;
    }

    public int JumpMotion(bool JampFlag)
    {
        animator.SetBool("JampFlag", JampFlag);
        return 0;
    }

    public int AttackMotion(bool AttackFlag)
    {

        animator.SetBool("Attack", AttackFlag);


        return 0;
    }




}
