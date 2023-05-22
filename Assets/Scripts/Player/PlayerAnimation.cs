using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    //private PlayerController player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
       
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
/*        if (player.isRun == true)
            anim.SetTrigger("run");*/
    }
}
