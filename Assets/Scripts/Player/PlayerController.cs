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
    public CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    //public��¶����ڣ�����ֱ���Ͻ�������privateֻ���ڴ����ڸ�ֵ
    public SpriteRenderer spriteRenderer;
    [Header("��������")]
    public float speed;
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;

    public bool isCrouch;
    private Vector2 originalOffset;
    private Vector2 originalSize;
    //public bool isRun = false;
    public bool isHurt;
    public float hurtForce;

    public bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        physicsCheck = GetComponent<PhysicsCheck>();

        coll = GetComponent<CapsuleCollider2D>();

        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;

        #region ǿ����·
        runSpeed = speed;
        //����K�������ܲ�
        inputControl.Gameplay.Run.performed += ctx => 
        {
            if (physicsCheck.isGround)
                speed = walkSpeed;
        };

        inputControl.Gameplay.Run.canceled += ctx =>
        {
            if (physicsCheck.isGround)
                speed = runSpeed;
        };
        #endregion
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
        if(!isHurt)
            Move();
    }

 

    public void Move()
    {
        if(!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0) 
            faceDir = -1;

        //���﷭ת
        transform.localScale = new Vector3(faceDir, 1, 1);

        //�¶�
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch)
        {
            //�޸���ײ���С��λ��
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            //��ԭ֮ǰ��ײ�����
            coll.size = originalSize;
            coll.offset = originalOffset;
        }

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

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }

    //run����ȥ֮�� isRunû�������߼�
/*    private void Run(InputAction.CallbackContext obj)
    {
        isRun = true;
    }*/
}

   