using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    [Header("Object reference")]
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [Header("Movement")]
    private float moveInput;
    public float speed;
    private bool isFacingRight = true;
    public ParticleSystem dust;
    
    [Header("Jump")]
    private bool isGrounded1, isGrounded2, isHanging;
    public bool isGrounded;
    private int jumpCount = 0;
    //private float jumpCooldown;

    public Transform feetPos1, feetPos2, nosePos; // folosite pentru a detecta daca caracterul este pe pamant
    public float checkRadius;
    [SerializeField] LayerMask whatIsGround; // lista cu ce este considerat "pamant"
    [SerializeField] int extraJumps = 0;
    [Range(1, 30)] 
    public float jumpForce;

    [Header("Animator")]
    public Animator animator;

    [Header("Wall Jump")] 
    public float wallJumpTime = 0.1f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float jumpTime;
    [SerializeField] private bool canWJ;
    private float lastDirection = 0, currentDirection = 0; //folosit pentru a nu sari pe acelasi perete de 2 ori la rand


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(moveInput*speed));

        
        if (isWallSliding)
        {
            animator.SetFloat("Speed", 0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput < 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector2(-1,1);
        }
        if(moveInput > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector2(1,1);
        }
        
        CheckGround();

        if (canWJ)
        {
            if (isFacingRight)
            {
                WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance,
                    whatIsGround);
                if (WallCheckHit && !isGrounded)
                {
                    currentDirection = 1; // dreapta
                }
                
                if (isGrounded)
                {
                    currentDirection = 0;
                }
                //Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.magenta);
            }
            else
            {
                WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance,
                    whatIsGround);
                if (WallCheckHit && !isGrounded)
                {
                    currentDirection = -1; // stanga
                }
                if (isGrounded)
                {
                    currentDirection = 0;
                }
                
                //Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.magenta);
            }
            if (WallCheckHit && !isGrounded && moveInput != 0)
            {
                isWallSliding = true;
                jumpTime = Time.time + wallJumpTime;
                PlayDust();
            }else if (jumpTime < Time.time)
            {
                isWallSliding = false;
            }
    
            if (isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
            }
        }
        
    }

    void Jump()
    {
        if (isGrounded || jumpCount < extraJumps || (isWallSliding && lastDirection != currentDirection))
        {
            lastDirection = currentDirection;
            rb.velocity = Vector2.up * jumpForce;
            
            jumpCount++;
        }
        
    }
    void CheckGround()
    {
        isGrounded1 = Physics2D.OverlapCircle(feetPos1.position, checkRadius, whatIsGround);
        isGrounded2 = Physics2D.OverlapCircle(feetPos2.position, checkRadius, whatIsGround); 
        isHanging = Physics2D.OverlapCircle(nosePos.position, checkRadius, whatIsGround);
        if (isGrounded1 == true || isGrounded2 == true || isHanging)
        {
            isGrounded = true;
            jumpCount = 0;
           // jumpCooldown = Time.time + 0.01f;
        }
        else if (extraJumps > jumpCount)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    void PlayDust()
    {
        dust.Play();
    }
}
