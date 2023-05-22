using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    //private PlayerController player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        //player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
       
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);

/*        if (player.isRun == true)
            anim.SetTrigger("run");*/
    }
}
