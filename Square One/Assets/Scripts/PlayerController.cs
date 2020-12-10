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
    private bool isGrounded1, isGrounded2;
    private float jumpTimeCounter;
    private bool isJumping;
    
    public float speed;
    public Transform feetPos1, feetPos2;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("test");
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput < 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
        if(moveInput > 0)
        {
            transform.localScale = new Vector2(1,1);
        }
        
        isGrounded1 = Physics2D.OverlapCircle(feetPos1.position, checkRadius, whatIsGround);
        isGrounded2 = Physics2D.OverlapCircle(feetPos2.position, checkRadius, whatIsGround);
        if ((isGrounded1 == true || isGrounded2 == true) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce; 
        }
    }
}
