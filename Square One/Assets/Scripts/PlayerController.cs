using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private float moveInput;
    private bool isGrounded1, isGrounded2, isGrounded;
    private int jumpCount = 0;
    private float jumpCooldown;
    
    public float speed;
    public Transform feetPos1, feetPos2;
    public float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    public float jumpForce;
    [SerializeField] int extraJumps = 0;
    public Animator animator;

    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(moveInput*speed));
        
        
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        CheckGround();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput < 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
        if(moveInput > 0)
        {
            transform.localScale = new Vector2(1,1);
        }
    }

    void Jump()
    {
        if (isGrounded || jumpCount < extraJumps)
        {
            Debug.Log(isGrounded);
            Debug.Log(jumpCount < extraJumps);
            rb.velocity = Vector2.up * jumpForce;
            jumpCount++;
        }
    }
    void CheckGround()
    {
        isGrounded1 = Physics2D.OverlapCircle(feetPos1.position, checkRadius, whatIsGround);
        isGrounded2 = Physics2D.OverlapCircle(feetPos2.position, checkRadius, whatIsGround);
        if (isGrounded1 == true || isGrounded2 == true)
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCooldown = Time.time + 0.2f;
        }else if (Time.time < jumpCooldown && extraJumps > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
