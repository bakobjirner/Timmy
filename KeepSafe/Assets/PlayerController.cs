using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float xMovement;
    private Rigidbody2D body;
    public float movementSpeed;
    public float jumpForce;
    public float wallJumpForce;

    public Transform playerBody;
    private bool isGrounded;
    private bool wallLeft;
    private bool wallRight;
    public Transform groundCheck;
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    private bool facedRight = true;
    public float punchRadius;
    private bool invincible;
    private float invincibleCounter;
    public float invincibleTime;
    private bool inBar;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move();
    }



    void Move()
    {


        if (body.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);
            
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
            body.velocity = new Vector2(xMovement, body.velocity.y);


            
               
            
           
       
            if ((facedRight && body.velocity.x < 0) || (!facedRight && body.velocity.x > 0))
            {
                flipDirection();
            }
        }
        else
        {
            
            if (isGrounded)
            {
                body.velocity = new Vector2(0, body.velocity.y); ;
            }
        }
        





    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        animator.SetBool("isJumping", !isGrounded);


        wallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, groundCheckRadius, groundLayer);
        wallRight = Physics2D.OverlapCircle(wallCheckRight.position, groundCheckRadius, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            GetComponentInChildren<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && wallLeft)
        {
            flipDirection();
            body.velocity = new Vector2(wallJumpForce, jumpForce);
            GetComponentInChildren<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && wallRight)
        {
            flipDirection();
            body.velocity = new Vector2(-wallJumpForce, jumpForce);
            GetComponentInChildren<AudioSource>().Play();
        }
    }
    void Update()
    {

        Jump();
        if (invincible)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                invincible = false;
            }
        }

    }

    void flipDirection()
    {
        facedRight = !facedRight;
        Vector3 Scaler = playerBody.localScale;
        Scaler.x *= -1;
        playerBody.localScale = Scaler;
       

    }

   





}
