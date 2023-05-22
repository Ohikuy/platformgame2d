using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Vector2 inputDirection;
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    //public暴露插入口，可以直接拖进来，但private只能在代码内赋值
    public SpriteRenderer spriteRenderer;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    //public bool isRun = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;
        //按下K键进行跑步
        //inputControl.Gameplay.Run.started += Run;

        //获得之前要获取
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);

        //通过spriteRenderer实现翻转的方式
        //批量注释快捷键ctrl+shift+/
/*        if (faceDir == 1)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;*/
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
        //Debug.Log("JUMP");
        if(physicsCheck.isGround == true)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //run按下去之后 isRun没有重置逻辑
/*    private void Run(InputAction.CallbackContext obj)
    {
        isRun = true;
    }*/
}

   