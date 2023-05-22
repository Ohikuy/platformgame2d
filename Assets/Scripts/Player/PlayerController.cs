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
    //public��¶����ڣ�����ֱ���Ͻ�������privateֻ���ڴ����ڸ�ֵ
    public SpriteRenderer spriteRenderer;
    [Header("��������")]
    public float speed;
    public float jumpForce;
    //public bool isRun = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;
        //����K�������ܲ�
        //inputControl.Gameplay.Run.started += Run;

        //���֮ǰҪ��ȡ
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

        //���﷭ת
        transform.localScale = new Vector3(faceDir, 1, 1);

        //ͨ��spriteRendererʵ�ַ�ת�ķ�ʽ
        //����ע�Ϳ�ݼ�ctrl+shift+/
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

    //run����ȥ֮�� isRunû�������߼�
/*    private void Run(InputAction.CallbackContext obj)
    {
        isRun = true;
    }*/
}

   