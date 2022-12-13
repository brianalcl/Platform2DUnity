using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    public float runSpeed;
    public float jumpSpeed;
    public bool betterJump;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    
    public PlayerMove()
    {
        runSpeed = 2;
        jumpSpeed = 3.5f;
        betterJump = true;
        fallMultiplier = 0.5f;
        lowJumpMultiplier = 1f;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            if(Input.GetKey("a") || Input.GetKey("left")){
                rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);
            }
            else
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
                animator.SetBool("Run", false);
            }
        }

        if (Input.GetKey("space") && CheckGround.grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }

        if (betterJump)
        {
            if(rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }

        }

        if (!CheckGround.grounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }
}
