using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterController2 : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float speed = 1.0f;
    public float moveDireciton;
    public float hurtvel=5.0f;

    private bool grounded=true;
    private bool jump;
    private bool moving;
    private bool hurt;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(rb.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        rb.velocity=new Vector2(speed*moveDireciton,rb.velocity.y);
        if(jump==true)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jump = false;
        }
        else if(hurt==true)
        {
           rb.velocity = new Vector2(rb.velocity.x, hurtvel);
           hurt = false;
        }


    }

    private void Update()
    {
        if(grounded==true && Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
        {
            if ((Input.GetKey(KeyCode.A)))
            {
                moveDireciton = -1.0f;
                spriteRenderer.flipX = true;
                animator.SetFloat("speed",speed);
                    
            }
            else if ((Input.GetKey(KeyCode.D)))
            {
                moveDireciton = 1.0f;
                spriteRenderer.flipX = false;
                animator.SetFloat("speed", speed);
            }
           
        }
        else if (grounded == true)
        {
            moveDireciton = 0.0f;
            animator.SetFloat("speed", 0.0f);
        }

        if(grounded == true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            animator.SetTrigger("jump");
            animator.SetBool("grounded", false);
        }

       else if (grounded == true && Input.GetKey(KeyCode.E))
        {
            hurt = true;
            grounded = false;
            animator.SetTrigger("hurt");
            animator.SetBool("grounded", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            animator.SetBool("grounded", true);
            grounded = true;
        }

        

    }
}
